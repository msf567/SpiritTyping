using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;
using Newtonsoft.Json;
using Newtonsoft.Json.Bson;

namespace SpiritTyping
{
    public class SpiritTypingProcessor : IDisposable
    {
        private bool _playing;

        public bool IsPlaying
        {
            get { return _playing; }
        }

        private bool _recording;

        public bool IsRecording
        {
            get { return _recording; }
        }

        public STScript CurrentScript = new STScript();

        //recording
        private DateTime commandStartedTime;

        //playback
        private Thread PlaybackThread;
        private double playbackCountMS;
        private int PlaybackIndex;
        public bool Verbose;
        private bool commandLock = false;
        private bool wasLocked = false;

        public delegate void NewCommandCallback(STFullCommand command, int index);

        public NewCommandCallback OnNewCommand;

        public void SaveCommands(string filePath)
        {
            FinishCurrentKeyboardCommand();

            var jsonData = JsonConvert.SerializeObject(CurrentScript, Formatting.None);

            var uncompressedBytes = Encoding.UTF8.GetBytes(jsonData);

            using (var compressedStream = new MemoryStream())
            {
                using (var gzipStream = new GZipStream(compressedStream, CompressionLevel.Optimal))
                {
                    gzipStream.Write(uncompressedBytes, 0, uncompressedBytes.Length);
                }

                File.WriteAllBytes(filePath, compressedStream.ToArray());
                Console.WriteLine($@"Saved {CurrentScript.Commands.Count} commands");
            }

            CurrentScript.Commands.Clear();
        }

        public void LoadCommands(string scriptName, string folderPath)
        {
            if (!Directory.Exists(folderPath))
                Directory.CreateDirectory(folderPath);
            string filePath = Path.Combine(folderPath, scriptName + ".spirit");
            //Console.WriteLine("Loading " + filePath);
            if (!File.Exists(filePath))
            {
                Console.WriteLine("doesn't exist");
                return;
            }

            byte[] compressedData = File.ReadAllBytes(filePath);

            using (var compressedStream = new MemoryStream(compressedData))
            using (var gzipStream = new GZipStream(compressedStream, CompressionMode.Decompress))
            using (var memoryStream = new MemoryStream())
            {
                gzipStream.CopyTo(memoryStream);

                var decompressedBytes = memoryStream.ToArray();
                var jsonData = Encoding.UTF8.GetString(decompressedBytes);

                CurrentScript = JsonConvert.DeserializeObject<STScript>(jsonData);

                foreach (var command in CurrentScript.Commands)
                {
                    //Console.WriteLine($@"Loaded {command.Text} {command.CursorPos} {command.HighlightLength} {command.DelayMS}ms");
                }
            }

            //Console.WriteLine($@"Loaded {CurrentScript.Commands.Count} commands");
            PlaybackIndex = 0;
        }

        public void StartRecording()
        {
            _recording = true;
            _playing = false;
            CurrentScript = new STScript();
        }

        public void StopRecording()
        {
            FinishCurrentKeyboardCommand();
            _recording = false;
        }

        public void StartPlayback()
        {
            _recording = false;
            _playing = true;
            playbackCountMS = 0;

            ExecuteCommand(CurrentScript.Commands[PlaybackIndex], PlaybackIndex);
            PlaybackIndex++;
            PlaybackThread?.Abort();
            PlaybackThread = new Thread(Playback);
            PlaybackThread.Start();
        }

        public void PausePlayback()
        {
            _playing = false;
            PlaybackThread?.Abort();
        }

        public void RecordCommand(string text, int cursorPos, int highlightLength, int Hx = 0, int Hy = 0)
        {
            if (!_recording)
                return;

            if (CurrentScript.Commands.Count > 0)
                FinishCurrentKeyboardCommand();
            commandStartedTime = DateTime.Now;

            var newCommand = new STFullCommand(text, cursorPos,
                highlightLength, Hx, Hy);

            if (CurrentScript.Commands.Count == 0)
                newCommand.Change = GetCommandDelta(new STFullCommand("", 0, 0), newCommand);
            else
                newCommand.Change = GetCommandDelta(CurrentScript.Commands.Last(), newCommand);

            CurrentScript.Commands.Add(newCommand);
            Console.WriteLine($@"Recording {text} {cursorPos} {highlightLength} {newCommand.Change}");
        }

        public void ModifyCommand(int index, string text, int cursorPos, int highlightLength, int delayMS,
            bool commandPause)
        {
            if (index >= CurrentScript.Commands.Count)
                return;

            CurrentScript.Commands[index].Text = text;
            CurrentScript.Commands[index].CursorPos = cursorPos;
            CurrentScript.Commands[index].HighlightLength = highlightLength;
            CurrentScript.Commands[index].DelayMS = delayMS;
            CurrentScript.Commands[index].WaitForCommand = commandPause;
        }

        public void UnlockCommand()
        {
            commandLock = false;
        }

        private void FinishCurrentKeyboardCommand()
        {
            if (CurrentScript.Commands.Count == 0)
            {
                Console.WriteLine("Tried to end a command when one was not in progress.");
                return;
            }

            TimeSpan delayTime = DateTime.Now - commandStartedTime;
            CurrentScript.Commands.Last().DelayMS = (int)delayTime.TotalMilliseconds;
            Console.WriteLine($@"Recorded at {delayTime.TotalMilliseconds} time.");
        }

        public void SkipToCommand(int index)
        {
            if (index >= CurrentScript.Commands.Count || index < 0)
                return;
            PlaybackIndex = index;
            ExecuteCommand(CurrentScript.Commands[index], index);
        }

        private void ExecuteCommand(STFullCommand c, int index)
        {
            OnNewCommand?.Invoke(c, index);
        }

        private void Playback()
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            while (_playing)
            {
                if (PlaybackIndex >= CurrentScript.Commands.Count)
                {
                    _playing = false;
                    //Console.WriteLine("Finished playback!");
                    return;
                }

                long elapsedMilliseconds = stopwatch.ElapsedMilliseconds;
                stopwatch.Restart();

                playbackCountMS += elapsedMilliseconds;
                if (Verbose)
                    Console.WriteLine(CurrentScript.Commands[PlaybackIndex].Text + " -- " + playbackCountMS + " vs " +
                                      CurrentScript.Commands[PlaybackIndex].DelayMS);
                bool waitingForCommandLock = commandLock && CurrentScript.Commands[PlaybackIndex].WaitForCommand;
                if (wasLocked && !waitingForCommandLock)
                {
                    playbackCountMS = 0;
                    //Console.WriteLine("Restarting stopwatch");
                    stopwatch.Restart();
                }

                wasLocked = waitingForCommandLock;
                int target = PlaybackIndex == 0 ? 0 : CurrentScript.Commands[PlaybackIndex - 1].DelayMS;
                if (playbackCountMS >= target && !waitingForCommandLock)
                {
                    commandLock = true;
                    playbackCountMS -= CurrentScript.Commands[PlaybackIndex].DelayMS;
                    ExecuteCommand(CurrentScript.Commands[PlaybackIndex], PlaybackIndex);
                    PlaybackIndex++;
                }

                Thread.Sleep(10);
            }
        }

        public void Dispose()
        {
            PlaybackThread?.Abort();
            _playing = false;
        }

        public string GetCommandDelta(STFullCommand lastCommand, STFullCommand newCommand)
        {
            if (lastCommand.Text != newCommand.Text)
            {
                string original = lastCommand.Text;
                string modified = newCommand.Text;
                if (original == modified)
                    return "";

                if (string.IsNullOrEmpty(original))
                    return "+" + modified;

                if (string.IsNullOrEmpty(modified))
                    return "-" + original;

                int startIndex = 0;
                while (startIndex < original.Length && startIndex < modified.Length &&
                       original[startIndex] == modified[startIndex])
                {
                    startIndex++;
                }

                int originalEnd = original.Length - 1;
                int modifiedEnd = modified.Length - 1;

                while (originalEnd >= startIndex && modifiedEnd >= startIndex &&
                       original[originalEnd] == modified[modifiedEnd])
                {
                    originalEnd--;
                    modifiedEnd--;
                }

                string removed = original.Substring(startIndex, originalEnd - startIndex + 1);
                string added = modified.Substring(startIndex, modifiedEnd - startIndex + 1);

                if (removed.Length > 10 || added.Length > 10)
                    return removed.Length > added.Length ? "-..." : "+...";

                if (removed.Length > 0 && added.Length > 0)
                    return "-" + removed + "+" + added;

                return removed.Length > 0 ? "-" + removed : "+" + added;
            }

            if (lastCommand.Text == newCommand.Text && lastCommand.CursorPos != newCommand.CursorPos)
            {
                return "Cursor Move";
            }

            if (lastCommand.Text == newCommand.Text && lastCommand.CursorPos == newCommand.CursorPos &&
                lastCommand.HighlightLength != newCommand.HighlightLength)
            {
                return "Highlight Change";
            }

            return "";
        }

        public void ReplaceTextInCommands(string findString, string replaceString)
        {
            if (findString != "")
                foreach (var command in CurrentScript.Commands)
                {
                    command.Text = command.Text.Replace(findString, replaceString);
                }

            RefreshCommandDeltas();
        }

        private void RefreshCommandDeltas()
        {
            CurrentScript.Commands[0].Change = GetCommandDelta(new STFullCommand("", 0, 0), CurrentScript.Commands[0]);
            for (int x = 1; x < CurrentScript.Commands.Count; x++)
            {
                CurrentScript.Commands[x].Change =
                    GetCommandDelta(CurrentScript.Commands[x - 1], CurrentScript.Commands[x]);
            }
        }
    }
}