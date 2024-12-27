using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;

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

        private List<STFullCommand> CommandBuffer = new List<STFullCommand>();

        //recording
        private DateTime commandStartedTime;

        //playback
        private Thread PlaybackThread;
        private double playbackCountMS;
        private int PlaybackIndex;

        public delegate void NewCommandCallback(STFullCommand command);

        public NewCommandCallback OnNewCommand;
        private IDisposable _disposableImplementation;

        public void SaveCommands(string filePath)
        {
            FinishCurrentKeyboardCommand();
            using (var memoryStream = new MemoryStream())
            {
                var formatter = new BinaryFormatter();
                formatter.Serialize(memoryStream, CommandBuffer);

                using (var compressedStream = new MemoryStream())
                {
                    using (var gzipStream = new GZipStream(compressedStream, CompressionLevel.Optimal))
                    {
                        gzipStream.Write(memoryStream.ToArray(), 0, (int)memoryStream.Length);
                    }

                    File.WriteAllBytes(filePath, compressedStream.ToArray());
                    Console.WriteLine($@"Saved {CommandBuffer.Count} commands");
                }
            }

            CommandBuffer.Clear();
        }

        public void LoadCommands(string filePath)
        {
            byte[] compressedData = File.ReadAllBytes(filePath);

            using (var compressedStream = new MemoryStream(compressedData))
            using (var gzipStream = new GZipStream(compressedStream, CompressionMode.Decompress))
            using (var memoryStream = new MemoryStream())
            {
                gzipStream.CopyTo(memoryStream);

                memoryStream.Seek(0, SeekOrigin.Begin);
                var formatter = new BinaryFormatter();
                CommandBuffer = (List<STFullCommand>)formatter.Deserialize(memoryStream);

                foreach (var command in CommandBuffer)
                {
                    Console.WriteLine(
                        $@"Loaded {command.Text} {command.CursorPos} {command.HighlightLength} {command.DelayMS}ms");
                }
            }

            Console.WriteLine($@"Loaded {CommandBuffer.Count} commands");
            PlaybackIndex = 0;
        }

        public void StartRecording()
        {
            _recording = true;
            _playing = false;
            CommandBuffer.Clear();
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
            PlaybackIndex = 0;
            playbackCountMS = 0;
            
            ExecuteCommand(CommandBuffer[PlaybackIndex++]);
            PlaybackThread?.Abort();
            PlaybackThread = new Thread(Playback);
            PlaybackThread.Start();
        }

        public void StopPlayback()
        {
            _playing = false;
            PlaybackThread?.Abort();
        }

        public void RecordCommand(string text, int cursorPos, int highlightLength)
        {
            if (!_recording)
                return;

            if (CommandBuffer.Count > 0)
                FinishCurrentKeyboardCommand();
            commandStartedTime = DateTime.Now;

            CommandBuffer.Add(new STFullCommand(text, cursorPos,
                highlightLength));
            Console.WriteLine($@"Recording {text} {cursorPos} {highlightLength}");
        }

        private void FinishCurrentKeyboardCommand()
        {
            if (CommandBuffer.Count == 0)
            {
                Console.WriteLine("Tried to end a command when one was not in progress.");
                return;
            }

            TimeSpan delayTime = DateTime.Now - commandStartedTime;
            CommandBuffer.Last().DelayMS = (int)delayTime.TotalMilliseconds;
            Console.WriteLine($@"Recorded at {delayTime.TotalMilliseconds} time.");
        }

        public void ExecuteNextCommand()
        {
            if (PlaybackIndex >= CommandBuffer.Count)
                return;
            ExecuteCommand(CommandBuffer[PlaybackIndex++]);
        }

        private void ExecuteCommand(STFullCommand c)
        {
            OnNewCommand?.Invoke(c);
        }

        private void Playback()
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            while (_playing)
            {
                long elapsedMilliseconds = stopwatch.ElapsedMilliseconds;
                stopwatch.Restart();
                
                playbackCountMS += elapsedMilliseconds; // Accumulate elapsed time

                Console.WriteLine(CommandBuffer[PlaybackIndex].Text + " -- " + playbackCountMS + " vs " +
                                  CommandBuffer[PlaybackIndex].DelayMS);
                if (playbackCountMS >= CommandBuffer[PlaybackIndex].DelayMS)
                {
                    playbackCountMS -= CommandBuffer[PlaybackIndex].DelayMS;
                    ExecuteCommand(CommandBuffer[PlaybackIndex++]);
                }

                if (PlaybackIndex >= CommandBuffer.Count)
                {
                    _playing = false;
                    Console.WriteLine("Finished playback!");
                    return;
                }

                Thread.Sleep(10); // Small pause
            }
        }

        public void Dispose()
        {
            PlaybackThread?.Abort();
            _playing = false;
        }
    }
}