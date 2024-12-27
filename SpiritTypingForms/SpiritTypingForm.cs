using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Newtonsoft.Json;
using System.IO.Compression;
using System.Runtime.Serialization.Formatters.Binary;

#pragma warning disable CS0169 // Field is never used

namespace SpiritTyping
{
    public partial class SpiritTypingForm : Form
    {
        private string lastText;
        private SpiritTypingProcessor processor = new();

        public SpiritTypingForm()
        {
            InitializeComponent();
            processor.OnNewCommand += ExecuteCommand;
        }

        private void TextEntryWindow_TextChanged(object sender, EventArgs e)
        {
            if (lastText == TextEntryWindow.Text)
                return;

            processor.RecordCommand(TextEntryWindow.Text, TextEntryWindow.SelectionStart,
                TextEntryWindow.SelectionLength);
            lastText = TextEntryWindow.Text;
        }

        private void TextEntryWindow_SelectionChanged(object sender, EventArgs e)
        {
            if (lastText != TextEntryWindow.Text)
                return;

            processor.RecordCommand(TextEntryWindow.Text, TextEntryWindow.SelectionStart,
                TextEntryWindow.SelectionLength);
        }

        private void ExecuteCommand(STFullCommand c)
        {
            UpdateTextControl(c.Text, c.CursorPos, c.HighlightLength);
        }

        private void UpdateTextControl(string text, int cursorPos, int highlightLength)
        {
            if (TextEntryWindow.InvokeRequired) // Check if we're on the wrong thread
            {
                TextEntryWindow.Invoke(new Action(() =>
                {
                    TextEntryWindow.Focus();
                    TextEntryWindow.Text = text;
                    TextEntryWindow.SelectionStart = cursorPos;
                    TextEntryWindow.SelectionLength = highlightLength;
                }));
            }
            else
            {
                TextEntryWindow.Focus();
                TextEntryWindow.Text = text;
                TextEntryWindow.SelectionStart = cursorPos;
                TextEntryWindow.SelectionLength = highlightLength;
            }
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            processor.SaveCommands("RecordedCommands.data");
        }

        private void ExecuteNextButton_Click(object sender, EventArgs e)
        {
            processor.ExecuteNextCommand();
        }

        private void HighlightAllButton_Click(object sender, EventArgs e)
        {
            TextEntryWindow.Focus();
            TextEntryWindow.SelectionStart = 0;
            TextEntryWindow.SelectionLength = TextEntryWindow.Text.Length - 1;
            TextEntryWindow.Refresh();
            Console.WriteLine(TextEntryWindow.SelectedText);
        }

        private void Playbutton_Click(object sender, EventArgs e)
        {
            processor.LoadCommands("RecordedCommands.data");
            processor.StartPlayback();
        }

        private void RecordButton_Click(object sender, EventArgs e)
        {
            processor.StartRecording();
        }

        private void LoadButton_Click(object sender, EventArgs e)
        {
            processor.LoadCommands("RecordedCommands.data");
        }
    }
}