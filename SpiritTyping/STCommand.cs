using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace SpiritTyping
{
    [Serializable]
    public class SpiritTypingState
    {
        public SpiritTypingState(string textToSet, int cursorPosition, int highlightLength, int hx = 0, int hy = 0)
        {
            Text = textToSet;
            CursorPos = cursorPosition;
            HighlightLength = highlightLength;
            Hx = hx;
            Hy = hy;
        }

        public string Text;
        public int CursorPos;
        public int HighlightLength;
        public double Time;
        public string Change;
        public int Hx, Hy;
    }

    [Serializable]
    public class STScript
    {
        public string Name = "";
        public string FilePath;
        public List<SpiritTypingState> Commands = new List<SpiritTypingState>();
        public TimeSpan Duration => TimeSpan.FromMilliseconds(Commands?.Last().Time ?? 0);

        public SpiritTypingState Resolve(double MS)
        {
            if (MS > Duration.TotalMilliseconds)
                return Commands.Last();
            if (MS <= 0)
                return Commands.First();

            return Commands.LastOrDefault(command => command.Time <= MS) ??
                   Commands.First();
        }

        public int GetCurrentIndex(double MS)
        {
            if (MS > Duration.TotalMilliseconds)
                return Commands.Count - 1;
            if (MS <= 0)
                return 0;
            return Commands.IndexOf(Commands.LastOrDefault(command => command.Time <= MS) ??
                                    Commands.First());
        }

        public void Save()
        {
            var jsonData = JsonConvert.SerializeObject(this, Formatting.None);

            var uncompressedBytes = Encoding.UTF8.GetBytes(jsonData);

            using (var compressedStream = new MemoryStream())
            {
                using (var gzipStream = new GZipStream(compressedStream, CompressionLevel.Optimal))
                {
                    gzipStream.Write(uncompressedBytes, 0, uncompressedBytes.Length);
                }

                File.WriteAllBytes(FilePath, compressedStream.ToArray());
                Console.WriteLine($@"Saved {Commands.Count} commands to " + FilePath);
            }
        }

        public static STScript Load(string scriptName, string folderPath)
        {
            STScript returnScript = new STScript();
            if (!Directory.Exists(folderPath))
                Directory.CreateDirectory(folderPath);
            string FilePath = Path.Combine(folderPath, scriptName + ".spirit");
            if (!File.Exists(FilePath))
            {
                Console.WriteLine("doesn't exist");
                return null;
            }

            byte[] compressedData = File.ReadAllBytes(FilePath);

            using (var compressedStream = new MemoryStream(compressedData))
            using (var gzipStream = new GZipStream(compressedStream, CompressionMode.Decompress))
            using (var memoryStream = new MemoryStream())
            {
                gzipStream.CopyTo(memoryStream);

                var decompressedBytes = memoryStream.ToArray();
                var jsonData = Encoding.UTF8.GetString(decompressedBytes);

                returnScript = JsonConvert.DeserializeObject<STScript>(jsonData);
                //Console.WriteLine(jsonData);
            }

            return returnScript;
        }

        public void BumpTimings(int index, double MSAmount, bool MoveEarlier)
        {
            Console.WriteLine("Bumping timings " + MSAmount + " starting from index " + index + " and moving all " +
                              (MoveEarlier ? "earlier" : "later") + " keyframes");
            if (index < 0 || index >= Commands.Count)
                return;

            if (!MoveEarlier)
            {
                if (index == Commands.Count - 1)
                    return;
                for (int x = index + 1; x < Commands.Count; x++)
                {
                    Commands[x].Time += MSAmount;
                    Console.WriteLine(
                        "Changed time from " + (Commands[x].Time - MSAmount) + " to " + Commands[x].Time);
                }
            }
            else
            {
                for (int x = index; x >= 0; x--)
                {
                    Commands[x].Time = Math.Max(0, Commands[x].Time + MSAmount);
                    Console.WriteLine(
                        "Changed time from " + (Commands[x].Time - MSAmount) + " to " + Commands[x].Time);
                }
            }
        }
    }
}