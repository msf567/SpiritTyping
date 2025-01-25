using System;

using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace SpiritTyping
{
    public class SpiritTypingProcessor : IDisposable
    {
        private bool _recording;

        public bool IsRecording
        {
            get { return _recording; }
        }

        private STScript ScriptInProgress = new STScript();

        //recording
        private DateTime RecordingStartTime;
        
        public void SaveCommands(string filePath)
        {
            ScriptInProgress.FilePath = filePath;
            ScriptInProgress.Name = filePath.Split('.')[0];
            var jsonData = JsonConvert.SerializeObject(ScriptInProgress, Formatting.None);

            var uncompressedBytes = Encoding.UTF8.GetBytes(jsonData);

            using (var compressedStream = new MemoryStream())
            {
                using (var gzipStream = new GZipStream(compressedStream, CompressionLevel.Optimal))
                {
                    gzipStream.Write(uncompressedBytes, 0, uncompressedBytes.Length);
                }

                File.WriteAllBytes(filePath, compressedStream.ToArray());
                Console.WriteLine($@"Saved {ScriptInProgress.Commands.Count} commands");
            }

            ScriptInProgress.Commands.Clear();
        }
        
        public void StartRecording(string initialText, int initialCursorPos, int initialHighlightLength, int iHx,
            int iHy)
        {
            _recording = true;
            ScriptInProgress = new STScript();
            RecordingStartTime = DateTime.Now;
            RecordCommand(initialText, initialCursorPos, initialHighlightLength, iHx, iHy);
        }

        public void StopRecording()
        {
            _recording = false;
        }

        public void RecordCommand(string text, int cursorPos, int highlightLength, int Hx = 0, int Hy = 0)
        {
            if (!_recording)
                return;

            var newCommand = new SpiritTypingState(text, cursorPos,
                highlightLength, Hx, Hy);
            //TODO keep a total pause time to enable pausing during recording for crazy moves
            newCommand.Time = (DateTime.Now - RecordingStartTime).TotalMilliseconds; 
            
            if (ScriptInProgress.Commands.Count == 0)
                newCommand.Change = GetCommandDelta(new SpiritTypingState("", 0, 0), newCommand);
            else
                newCommand.Change = GetCommandDelta(ScriptInProgress.Commands.Last(), newCommand);

            ScriptInProgress.Commands.Add(newCommand);
            Console.WriteLine(
                $@"Recording {text} {cursorPos} {highlightLength} {newCommand.Change} at {newCommand.Time}");
        }

        public void ModifyCommand(int index, string text, int cursorPos, int highlightLength, int time)
        {
            if (index >= ScriptInProgress.Commands.Count)
                return;

            ScriptInProgress.Commands[index].Text = text;
            ScriptInProgress.Commands[index].CursorPos = cursorPos;
            ScriptInProgress.Commands[index].HighlightLength = highlightLength;
            ScriptInProgress.Commands[index].Time = time;
        }

        public void Dispose()
        {
        }

        public string GetCommandDelta(SpiritTypingState lastCommand, SpiritTypingState newCommand)
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
                foreach (var command in ScriptInProgress.Commands)
                {
                    command.Text = command.Text.Replace(findString, replaceString);
                }

            RefreshCommandDeltas();
        }

        private void RefreshCommandDeltas()
        {
            ScriptInProgress.Commands[0].Change =
                GetCommandDelta(new SpiritTypingState("", 0, 0), ScriptInProgress.Commands[0]);
            for (int x = 1; x < ScriptInProgress.Commands.Count; x++)
            {
                ScriptInProgress.Commands[x].Change =
                    GetCommandDelta(ScriptInProgress.Commands[x - 1], ScriptInProgress.Commands[x]);
            }
        }
    }
}