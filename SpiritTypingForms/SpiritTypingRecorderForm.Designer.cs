namespace SpiritTyping
{
    partial class SpiritTypingRecorderForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }

            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.TextEntryWindow = new System.Windows.Forms.RichTextBox();
            this.SaveButton = new System.Windows.Forms.Button();
            this.PlayButton = new System.Windows.Forms.Button();
            this.RecordButton = new System.Windows.Forms.Button();
            this.CommandListView = new System.Windows.Forms.DataGridView();
            this.PlaybackBar = new System.Windows.Forms.TrackBar();
            this.BackButton = new System.Windows.Forms.Button();
            this.ForwardButton = new System.Windows.Forms.Button();
            this.WriteButton = new System.Windows.Forms.Button();
            this.DelayTextBox = new System.Windows.Forms.TextBox();
            this.DelayLabel = new System.Windows.Forms.Label();
            this.OldTextBox = new System.Windows.Forms.RichTextBox();
            this.ReplaceTextBox = new System.Windows.Forms.RichTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.ReplaceButton = new System.Windows.Forms.Button();
            this.TabControl = new System.Windows.Forms.TabControl();
            this.LoadedScriptTabPage = new System.Windows.Forms.TabPage();
            this.ScriptLibraryTabPage = new System.Windows.Forms.TabPage();
            this.ScriptLibraryListView = new System.Windows.Forms.DataGridView();
            this.LoadedScriptLabel = new System.Windows.Forms.Label();
            this.DurationLabel = new System.Windows.Forms.Label();
            this.CommandPauseToggle = new System.Windows.Forms.CheckBox();
            this.CommandButton = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.CursorPosTextBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.HighlightLengthTextBox = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.CommandListView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PlaybackBar)).BeginInit();
            this.TabControl.SuspendLayout();
            this.LoadedScriptTabPage.SuspendLayout();
            this.ScriptLibraryTabPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ScriptLibraryListView)).BeginInit();
            this.SuspendLayout();
            // 
            // TextEntryWindow
            // 
            this.TextEntryWindow.Location = new System.Drawing.Point(12, 44);
            this.TextEntryWindow.Name = "TextEntryWindow";
            this.TextEntryWindow.Size = new System.Drawing.Size(565, 394);
            this.TextEntryWindow.TabIndex = 0;
            this.TextEntryWindow.Text = "";
            this.TextEntryWindow.SelectionChanged += new System.EventHandler(this.TextEntryWindow_SelectionChanged);
            this.TextEntryWindow.TextChanged += new System.EventHandler(this.TextEntryWindow_TextChanged);
            // 
            // SaveButton
            // 
            this.SaveButton.Location = new System.Drawing.Point(372, 15);
            this.SaveButton.Name = "SaveButton";
            this.SaveButton.Size = new System.Drawing.Size(205, 23);
            this.SaveButton.TabIndex = 1;
            this.SaveButton.Text = "Save";
            this.SaveButton.UseVisualStyleBackColor = true;
            this.SaveButton.Click += new System.EventHandler(this.SaveButton_Click);
            // 
            // PlayButton
            // 
            this.PlayButton.Location = new System.Drawing.Point(466, 444);
            this.PlayButton.Name = "PlayButton";
            this.PlayButton.Size = new System.Drawing.Size(111, 23);
            this.PlayButton.TabIndex = 2;
            this.PlayButton.Text = "Play";
            this.PlayButton.UseVisualStyleBackColor = true;
            this.PlayButton.Click += new System.EventHandler(this.PlayButton_Click);
            // 
            // RecordButton
            // 
            this.RecordButton.Location = new System.Drawing.Point(12, 12);
            this.RecordButton.Name = "RecordButton";
            this.RecordButton.Size = new System.Drawing.Size(205, 23);
            this.RecordButton.TabIndex = 5;
            this.RecordButton.Text = "Record";
            this.RecordButton.UseVisualStyleBackColor = true;
            this.RecordButton.Click += new System.EventHandler(this.RecordButton_Click);
            // 
            // CommandListView
            // 
            this.CommandListView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.CommandListView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.CommandListView.Location = new System.Drawing.Point(3, 3);
            this.CommandListView.Name = "CommandListView";
            this.CommandListView.RowHeadersVisible = false;
            this.CommandListView.RowHeadersWidth = 5;
            this.CommandListView.RowTemplate.Height = 24;
            this.CommandListView.Size = new System.Drawing.Size(219, 643);
            this.CommandListView.TabIndex = 7;
            // 
            // PlaybackBar
            // 
            this.PlaybackBar.Location = new System.Drawing.Point(50, 473);
            this.PlaybackBar.Name = "PlaybackBar";
            this.PlaybackBar.Size = new System.Drawing.Size(489, 56);
            this.PlaybackBar.TabIndex = 8;
            this.PlaybackBar.Scroll += new System.EventHandler(this.PlaybackBar_Scroll);
            // 
            // BackButton
            // 
            this.BackButton.Location = new System.Drawing.Point(12, 473);
            this.BackButton.Name = "BackButton";
            this.BackButton.Size = new System.Drawing.Size(32, 56);
            this.BackButton.TabIndex = 9;
            this.BackButton.Text = "<";
            this.BackButton.UseVisualStyleBackColor = true;
            this.BackButton.Click += new System.EventHandler(this.BackButton_Click);
            // 
            // ForwardButton
            // 
            this.ForwardButton.Location = new System.Drawing.Point(545, 473);
            this.ForwardButton.Name = "ForwardButton";
            this.ForwardButton.Size = new System.Drawing.Size(32, 56);
            this.ForwardButton.TabIndex = 10;
            this.ForwardButton.Text = ">";
            this.ForwardButton.UseVisualStyleBackColor = true;
            this.ForwardButton.Click += new System.EventHandler(this.ForwardButton_Click);
            // 
            // WriteButton
            // 
            this.WriteButton.Location = new System.Drawing.Point(232, 444);
            this.WriteButton.Name = "WriteButton";
            this.WriteButton.Size = new System.Drawing.Size(113, 23);
            this.WriteButton.TabIndex = 11;
            this.WriteButton.Text = "Write";
            this.WriteButton.UseVisualStyleBackColor = true;
            this.WriteButton.Click += new System.EventHandler(this.WriteButton_Click);
            // 
            // DelayTextBox
            // 
            this.DelayTextBox.Location = new System.Drawing.Point(13, 615);
            this.DelayTextBox.Name = "DelayTextBox";
            this.DelayTextBox.Size = new System.Drawing.Size(100, 22);
            this.DelayTextBox.TabIndex = 12;
            // 
            // DelayLabel
            // 
            this.DelayLabel.Location = new System.Drawing.Point(13, 589);
            this.DelayLabel.Name = "DelayLabel";
            this.DelayLabel.Size = new System.Drawing.Size(100, 23);
            this.DelayLabel.TabIndex = 13;
            this.DelayLabel.Text = "Delay";
            this.DelayLabel.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // OldTextBox
            // 
            this.OldTextBox.Location = new System.Drawing.Point(13, 659);
            this.OldTextBox.Name = "OldTextBox";
            this.OldTextBox.Size = new System.Drawing.Size(213, 73);
            this.OldTextBox.TabIndex = 14;
            this.OldTextBox.Text = "";
            // 
            // ReplaceTextBox
            // 
            this.ReplaceTextBox.Location = new System.Drawing.Point(365, 659);
            this.ReplaceTextBox.Name = "ReplaceTextBox";
            this.ReplaceTextBox.Size = new System.Drawing.Size(213, 73);
            this.ReplaceTextBox.TabIndex = 15;
            this.ReplaceTextBox.Text = "";
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(13, 633);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 23);
            this.label1.TabIndex = 16;
            this.label1.Text = "Old Text";
            this.label1.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(365, 633);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(100, 23);
            this.label2.TabIndex = 17;
            this.label2.Text = "Replacement";
            this.label2.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // ReplaceButton
            // 
            this.ReplaceButton.Location = new System.Drawing.Point(232, 659);
            this.ReplaceButton.Name = "ReplaceButton";
            this.ReplaceButton.Size = new System.Drawing.Size(127, 34);
            this.ReplaceButton.TabIndex = 18;
            this.ReplaceButton.Text = "Replace";
            this.ReplaceButton.UseVisualStyleBackColor = true;
            this.ReplaceButton.Click += new System.EventHandler(this.ReplaceButton_Click);
            // 
            // TabControl
            // 
            this.TabControl.Controls.Add(this.LoadedScriptTabPage);
            this.TabControl.Controls.Add(this.ScriptLibraryTabPage);
            this.TabControl.Location = new System.Drawing.Point(583, 15);
            this.TabControl.Name = "TabControl";
            this.TabControl.SelectedIndex = 0;
            this.TabControl.Size = new System.Drawing.Size(233, 678);
            this.TabControl.TabIndex = 19;
            // 
            // LoadedScriptTabPage
            // 
            this.LoadedScriptTabPage.Controls.Add(this.CommandListView);
            this.LoadedScriptTabPage.Location = new System.Drawing.Point(4, 25);
            this.LoadedScriptTabPage.Name = "LoadedScriptTabPage";
            this.LoadedScriptTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.LoadedScriptTabPage.Size = new System.Drawing.Size(225, 649);
            this.LoadedScriptTabPage.TabIndex = 0;
            this.LoadedScriptTabPage.Text = "LoadedScript";
            this.LoadedScriptTabPage.UseVisualStyleBackColor = true;
            // 
            // ScriptLibraryTabPage
            // 
            this.ScriptLibraryTabPage.Controls.Add(this.ScriptLibraryListView);
            this.ScriptLibraryTabPage.Location = new System.Drawing.Point(4, 25);
            this.ScriptLibraryTabPage.Name = "ScriptLibraryTabPage";
            this.ScriptLibraryTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.ScriptLibraryTabPage.Size = new System.Drawing.Size(225, 649);
            this.ScriptLibraryTabPage.TabIndex = 1;
            this.ScriptLibraryTabPage.Text = "ScriptLibrary";
            this.ScriptLibraryTabPage.UseVisualStyleBackColor = true;
            // 
            // ScriptLibraryListView
            // 
            this.ScriptLibraryListView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ScriptLibraryListView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ScriptLibraryListView.Location = new System.Drawing.Point(3, 3);
            this.ScriptLibraryListView.Name = "ScriptLibraryListView";
            this.ScriptLibraryListView.RowHeadersVisible = false;
            this.ScriptLibraryListView.RowHeadersWidth = 5;
            this.ScriptLibraryListView.RowTemplate.Height = 24;
            this.ScriptLibraryListView.Size = new System.Drawing.Size(219, 643);
            this.ScriptLibraryListView.TabIndex = 0;
            // 
            // LoadedScriptLabel
            // 
            this.LoadedScriptLabel.Location = new System.Drawing.Point(587, 696);
            this.LoadedScriptLabel.Name = "LoadedScriptLabel";
            this.LoadedScriptLabel.Size = new System.Drawing.Size(100, 23);
            this.LoadedScriptLabel.TabIndex = 20;
            this.LoadedScriptLabel.Text = "Loaded Script";
            // 
            // DurationLabel
            // 
            this.DurationLabel.Location = new System.Drawing.Point(709, 696);
            this.DurationLabel.Name = "DurationLabel";
            this.DurationLabel.Size = new System.Drawing.Size(100, 23);
            this.DurationLabel.TabIndex = 21;
            this.DurationLabel.Text = "00:00:00";
            this.DurationLabel.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // CommandPauseToggle
            // 
            this.CommandPauseToggle.Location = new System.Drawing.Point(12, 535);
            this.CommandPauseToggle.Name = "CommandPauseToggle";
            this.CommandPauseToggle.Size = new System.Drawing.Size(134, 24);
            this.CommandPauseToggle.TabIndex = 22;
            this.CommandPauseToggle.Text = "CommandPause";
            this.CommandPauseToggle.UseVisualStyleBackColor = true;
            // 
            // CommandButton
            // 
            this.CommandButton.Location = new System.Drawing.Point(232, 535);
            this.CommandButton.Name = "CommandButton";
            this.CommandButton.Size = new System.Drawing.Size(113, 45);
            this.CommandButton.TabIndex = 23;
            this.CommandButton.Text = "Trigger Command";
            this.CommandButton.UseVisualStyleBackColor = true;
            this.CommandButton.Click += new System.EventHandler(this.CommandButton_Click);
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(365, 589);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(100, 23);
            this.label3.TabIndex = 25;
            this.label3.Text = "CPos";
            this.label3.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // CursorPosTextBox
            // 
            this.CursorPosTextBox.Location = new System.Drawing.Point(365, 615);
            this.CursorPosTextBox.Name = "CursorPosTextBox";
            this.CursorPosTextBox.Size = new System.Drawing.Size(100, 22);
            this.CursorPosTextBox.TabIndex = 24;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(477, 589);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(100, 23);
            this.label4.TabIndex = 27;
            this.label4.Text = "HLen";
            this.label4.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // HighlightLengthTextBox
            // 
            this.HighlightLengthTextBox.Location = new System.Drawing.Point(477, 615);
            this.HighlightLengthTextBox.Name = "HighlightLengthTextBox";
            this.HighlightLengthTextBox.Size = new System.Drawing.Size(100, 22);
            this.HighlightLengthTextBox.TabIndex = 26;
            // 
            // SpiritTypingRecorderForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(818, 741);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.HighlightLengthTextBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.CursorPosTextBox);
            this.Controls.Add(this.CommandButton);
            this.Controls.Add(this.CommandPauseToggle);
            this.Controls.Add(this.DurationLabel);
            this.Controls.Add(this.LoadedScriptLabel);
            this.Controls.Add(this.TabControl);
            this.Controls.Add(this.ReplaceButton);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ReplaceTextBox);
            this.Controls.Add(this.OldTextBox);
            this.Controls.Add(this.DelayLabel);
            this.Controls.Add(this.DelayTextBox);
            this.Controls.Add(this.WriteButton);
            this.Controls.Add(this.ForwardButton);
            this.Controls.Add(this.BackButton);
            this.Controls.Add(this.PlaybackBar);
            this.Controls.Add(this.RecordButton);
            this.Controls.Add(this.PlayButton);
            this.Controls.Add(this.SaveButton);
            this.Controls.Add(this.TextEntryWindow);
            this.Name = "SpiritTypingRecorderForm";
            this.Text = "SpiritTypingRecorder";
            ((System.ComponentModel.ISupportInitialize)(this.CommandListView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PlaybackBar)).EndInit();
            this.TabControl.ResumeLayout(false);
            this.LoadedScriptTabPage.ResumeLayout(false);
            this.ScriptLibraryTabPage.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ScriptLibraryListView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox CursorPosTextBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox HighlightLengthTextBox;

        private System.Windows.Forms.DataGridView ScriptLibraryListView;

        private System.Windows.Forms.Button CommandButton;

        private System.Windows.Forms.CheckBox CommandPauseToggle;

        private System.Windows.Forms.Label LoadedScriptLabel;
        private System.Windows.Forms.Label DurationLabel;

        private System.Windows.Forms.TabControl TabControl;
        private System.Windows.Forms.TabPage LoadedScriptTabPage;
        private System.Windows.Forms.TabPage ScriptLibraryTabPage;

        private System.Windows.Forms.Button ReplaceButton;

        private System.Windows.Forms.RichTextBox OldTextBox;
        private System.Windows.Forms.RichTextBox ReplaceTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;

        private System.Windows.Forms.TextBox DelayTextBox;
        private System.Windows.Forms.Label DelayLabel;

        private System.Windows.Forms.Button WriteButton;

        private System.Windows.Forms.Button BackButton;
        private System.Windows.Forms.Button ForwardButton;

        private System.Windows.Forms.TrackBar PlaybackBar;

        private System.Windows.Forms.DataGridView CommandListView;

        private System.Windows.Forms.Button RecordButton;

        private System.Windows.Forms.Button PlayButton;

        private System.Windows.Forms.Button SaveButton;
        
        private System.Windows.Forms.RichTextBox TextEntryWindow;

        #endregion
    }
}