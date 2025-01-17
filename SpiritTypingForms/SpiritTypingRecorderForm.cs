using System;
using System.IO;
using System.Windows.Forms;

#pragma warning disable CS0169 // Field is never used

namespace SpiritTyping
{
    public partial class SpiritTypingRecorderForm : Form
    {
        private string lastText;
        private SpiritTypingProcessor processor = new();

        public SpiritTypingRecorderForm()
        {
            InitializeComponent();
            processor.OnNewCommand += ExecuteCommand;
            ReloadScriptLibrary();
        }

        private void TextEntryWindow_TextChanged(object sender, EventArgs e)
        {
            if (lastText == TextEntryWindow.Text)
                return;

            var res = GetHighlightEndPosition(TextEntryWindow.Text, TextEntryWindow.SelectionStart,
                TextEntryWindow.SelectionLength);
            
            processor.RecordCommand(TextEntryWindow.Text, TextEntryWindow.SelectionStart,
                TextEntryWindow.SelectionLength,res.x,res.y);
            lastText = TextEntryWindow.Text;
        }

        private void TextEntryWindow_SelectionChanged(object sender, EventArgs e)
        {
            if (lastText != TextEntryWindow.Text)
                return;

            var res = GetHighlightEndPosition(TextEntryWindow.Text, TextEntryWindow.SelectionStart,
                TextEntryWindow.SelectionLength);
            
            processor.RecordCommand(TextEntryWindow.Text, TextEntryWindow.SelectionStart,
                TextEntryWindow.SelectionLength, res.x, res.y);
        }

        static (int x, int y) GetHighlightEndPosition(string text, int cursorPos, int highlightLength)
        {
            int endPos = cursorPos + highlightLength;
            int x = 0, y = 0, currentPos = 0;

            foreach (var line in text.Split('\n'))
            {
                int lineLength = line.Length + 1; // Include newline character

                if (currentPos + lineLength > endPos)
                {
                    x = endPos - currentPos;
                    break;
                }

                currentPos += lineLength;
                y++;
            }

            return (x, y);
        }
        
        private void ExecuteCommand(STFullCommand c, int index)
        {
            UpdateTextControl(c.Text, c.CursorPos, c.HighlightLength);
            SetTrackbarIndex(index);
            SetSelectedCommandInList(index);
            SetDelayBox(c.DelayMS);
            SetHighlightBox(c.HighlightLength);
            SetCursorPosBox(c.CursorPos);
            SetCommandToggle(c.WaitForCommand);
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
            var d = new SaveFileDialog();
            d.DefaultExt = ".spirit";
            d.ShowDialog();
            processor.SaveCommands(d.FileName);
            Text = @"SpiritTypingRecorder" + (processor.IsRecording ? " [RECORDING]" : "");
        }

        private void PlayButton_Click(object sender, EventArgs e)
        {
            Console.WriteLine("button pressed");
            if (processor.CurrentScript.Commands.Count == 0)
                return;

            if (processor.IsPlaying)
            {
                Console.WriteLine(@"Trying to pause");
                processor.PausePlayback();
            }
            else
            {
                Console.WriteLine(@"Trying to play");
                processor.StartPlayback();
            }

            PlayButton.Text = processor.IsPlaying ? "Pause" : "Play";
            Text = @"SpiritTypingRecorder" + (processor.IsRecording ? " [RECORDING]" : "");
        }

        private void RecordButton_Click(object sender, EventArgs e)
        {
            if (processor.IsRecording)
            {
                processor.StopRecording();
            }
            else
            {
                processor.StartRecording();
                TextEntryWindow.Focus();
            }

            Text = @"SpiritTypingRecorder" + (processor.IsRecording ? " [RECORDING]" : "");

            RecordButton.Text = processor.IsRecording ? "Stop Recording" : "Record";
        }

        private void ReloadScriptLibrary()
        {
            if (ScriptLibraryListView.Columns.Count == 0)
            {
                var scriptColumn = new DataGridViewButtonColumn
                {
                    HeaderText = @"Scripts",
                    Name = "Scripts",
                    Text = "",
                    Width = ScriptLibraryListView.Width - 20,
                };
                ScriptLibraryListView.Columns.Add(scriptColumn);
            }

            ScriptLibraryListView.Rows.Clear();
            DirectoryInfo d = new DirectoryInfo("Q:\\Snow\\Builds\\SpiritScripts");
            foreach (var f in d.GetFiles())
            {
                ScriptLibraryListView.Rows.Add(f.Name);
            }

            ScriptLibraryListView.CellContentClick += ScriptLibraryListViewOnCellContentClick;
        }

        private void ScriptLibraryListViewOnCellContentClick(object _sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex == 0)
            {
                var name = ScriptLibraryListView.Rows[e.RowIndex].Cells[0].Value
                    ?.ToString(); // Assuming script names are in column 1
                if (!string.IsNullOrEmpty(name))
                {
                    processor.LoadCommands(name.Split('.')[0], "Q:\\Snow\\Builds\\SpiritScripts");
                    LoadedScriptLabel.Text = processor.CurrentScript.Name;
                    DurationLabel.Text = processor.CurrentScript.Duration.ToString(@"mm\:ss\:fff");
                    Text = @"SpiritTypingRecorder" + (processor.IsRecording ? " [RECORDING]" : "");
                    DisplayCommandList();
                    processor.SkipToCommand(0);
                }
            }
        }

        private void InitializeCommandList()
        {
            var buttonColumn = new DataGridViewButtonColumn
            {
                HeaderText = @"Commands",
                Name = "Commands",
                Text = "",
                Width = CommandListView.Width - 20,
            };
            CommandListView.Columns.Add(buttonColumn);
            CommandListView.CellContentClick += DataGridView_CellContentClick;
        }

        private void DataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex == CommandListView.Columns["Commands"].Index)
            {
                int rowIndex = e.RowIndex;
                processor.SkipToCommand(rowIndex);
            }
        }

        private void DisplayCommandList()
        {
            if (CommandListView.Columns.Count == 0)
                InitializeCommandList();
            CommandListView.Rows.Clear();
            foreach (var command in processor.CurrentScript.Commands)
            {
                CommandListView.Rows.Add(command.Change);
            }

            PlaybackBar.Minimum = 0;
            PlaybackBar.Maximum = processor.CurrentScript.Commands.Count - 1;
        }

        private void SetTrackbarIndex(int index)
        {
            if (PlaybackBar.InvokeRequired) // Check if we're on the wrong thread
            {
                PlaybackBar.Invoke(new Action(() => { PlaybackBar.Value = index; }));
            }
            else
            {
                PlaybackBar.Value = index;
            }
        }

        private void SetSelectedCommandInList(int index)
        {
            if (CommandListView.InvokeRequired)
            {
                CommandListView.Invoke(new Action(() =>
                {
                    CommandListView.CurrentCell = CommandListView.Rows[index].Cells[0];
                }));
            }
            else
            {
                CommandListView.CurrentCell = CommandListView.Rows[index].Cells[0];
            }
        }

        private void SetDelayBox(int delayMS)
        {
            if (DelayTextBox.InvokeRequired)
            {
                DelayTextBox.Invoke(new Action(() => { DelayTextBox.Text = delayMS.ToString(); }));
            }
            else
            {
                DelayTextBox.Text = delayMS.ToString();
            }
        }

        private void SetHighlightBox(int highlightLength)
        {
            if (HighlightLengthTextBox.InvokeRequired)
            {
                HighlightLengthTextBox.Invoke(new Action(() =>
                {
                    HighlightLengthTextBox.Text = highlightLength.ToString();
                }));
            }
            else
            {
                HighlightLengthTextBox.Text = highlightLength.ToString();
            }
        }

        private void SetCursorPosBox(int cursorPos)
        {
            if (CursorPosTextBox.InvokeRequired)
            {
                CursorPosTextBox.Invoke(new Action(() => { CursorPosTextBox.Text = cursorPos.ToString(); }));
            }
            else
            {
                CursorPosTextBox.Text = cursorPos.ToString();
            }
        }

        private void SetCommandToggle(bool enabled)
        {
            if (CommandPauseToggle.InvokeRequired)
            {
                CommandPauseToggle.Invoke(new Action(() => { CommandPauseToggle.Checked = enabled; }));
            }
            else
            {
                CommandPauseToggle.Checked = enabled;
            }
        }

        private void PlaybackBar_Scroll(object sender, EventArgs e)
        {
            if (processor.CurrentScript.Commands.Count == 0)
                return;
            processor.PausePlayback();
            PlayButton.Text = "Play";
            processor.SkipToCommand(PlaybackBar.Value);
        }

        private void BackButton_Click(object sender, EventArgs e)
        {
            processor.SkipToCommand(PlaybackBar.Value - 1);
        }

        private void ForwardButton_Click(object sender, EventArgs e)
        {
            processor.SkipToCommand(PlaybackBar.Value + 1);
        }

        private void WriteButton_Click(object sender, EventArgs e)
        {
            var currentCommand = processor.CurrentScript.Commands[PlaybackBar.Value];
            int newDelayMS;
            if (int.TryParse(DelayTextBox.Text, out newDelayMS))
            {
                processor.ModifyCommand(PlaybackBar.Value,
                    currentCommand.Text,
                    currentCommand.CursorPos,
                    currentCommand.HighlightLength,
                    newDelayMS,
                    CommandPauseToggle.Checked);
            }
        }

        private void ReplaceButton_Click(object sender, EventArgs e)
        {
            processor.ReplaceTextInCommands(OldTextBox.Text, ReplaceTextBox.Text);
            DisplayCommandList();
            processor.SkipToCommand(PlaybackBar.Value);
        }

        private void CommandButton_Click(object sender, EventArgs e)
        {
            processor.UnlockCommand();
        }
    }
}