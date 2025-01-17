using System;
using System.Collections.Generic;
using System.Linq;

namespace SpiritTyping
{
    [Serializable]
    public class STFullCommand
    {
        public STFullCommand(string textToSet, int cursorPosition, int highlightLength, int hx = 0, int hy = 0)
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
        public int DelayMS;
        public string Change;
        public bool WaitForCommand;
        public int Hx, Hy;
    }


    [Serializable]
    public class STScript
    {
        public string Name = "";
        public List<STFullCommand> Commands = new List<STFullCommand>();
        public TimeSpan Duration => TimeSpan.FromMilliseconds(Commands?.Sum(command => command.DelayMS) ?? 0);
    }
}