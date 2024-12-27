using System;

namespace SpiritTyping
{
    [Serializable]
    public class STFullCommand
    {
        public STFullCommand(string textToSet, int cursorPosition, int highlightLength)
        {
            Text = textToSet;
            CursorPos = cursorPosition;
            HighlightLength = highlightLength;
        }

        public string Text;
        public int CursorPos;
        public int HighlightLength;
        public int DelayMS;
    }
}