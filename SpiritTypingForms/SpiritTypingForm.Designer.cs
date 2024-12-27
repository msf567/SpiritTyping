namespace SpiritTyping
{
    partial class SpiritTypingForm
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
            this.Playbutton = new System.Windows.Forms.Button();
            this.ExecuteNextButton = new System.Windows.Forms.Button();
            this.HighlightAllButton = new System.Windows.Forms.Button();
            this.RecordButton = new System.Windows.Forms.Button();
            this.LoadButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // TextEntryWindow
            // 
            this.TextEntryWindow.Location = new System.Drawing.Point(12, 12);
            this.TextEntryWindow.Name = "TextEntryWindow";
            this.TextEntryWindow.Size = new System.Drawing.Size(565, 426);
            this.TextEntryWindow.TabIndex = 0;
            this.TextEntryWindow.Text = "";
            this.TextEntryWindow.SelectionChanged += new System.EventHandler(this.TextEntryWindow_SelectionChanged);
            this.TextEntryWindow.TextChanged += new System.EventHandler(this.TextEntryWindow_TextChanged);
            // 
            // SaveButton
            // 
            this.SaveButton.Location = new System.Drawing.Point(583, 76);
            this.SaveButton.Name = "SaveButton";
            this.SaveButton.Size = new System.Drawing.Size(205, 23);
            this.SaveButton.TabIndex = 1;
            this.SaveButton.Text = "Save";
            this.SaveButton.UseVisualStyleBackColor = true;
            this.SaveButton.Click += new System.EventHandler(this.SaveButton_Click);
            // 
            // Playbutton
            // 
            this.Playbutton.Location = new System.Drawing.Point(583, 105);
            this.Playbutton.Name = "Playbutton";
            this.Playbutton.Size = new System.Drawing.Size(205, 23);
            this.Playbutton.TabIndex = 2;
            this.Playbutton.Text = "Play";
            this.Playbutton.UseVisualStyleBackColor = true;
            this.Playbutton.Click += new System.EventHandler(this.Playbutton_Click);
            // 
            // ExecuteNextButton
            // 
            this.ExecuteNextButton.Location = new System.Drawing.Point(583, 134);
            this.ExecuteNextButton.Name = "ExecuteNextButton";
            this.ExecuteNextButton.Size = new System.Drawing.Size(205, 23);
            this.ExecuteNextButton.TabIndex = 3;
            this.ExecuteNextButton.Text = "Execute Next";
            this.ExecuteNextButton.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.ExecuteNextButton.UseVisualStyleBackColor = true;
            this.ExecuteNextButton.Click += new System.EventHandler(this.ExecuteNextButton_Click);
            // 
            // HighlightAllButton
            // 
            this.HighlightAllButton.Location = new System.Drawing.Point(583, 163);
            this.HighlightAllButton.Name = "HighlightAllButton";
            this.HighlightAllButton.Size = new System.Drawing.Size(205, 26);
            this.HighlightAllButton.TabIndex = 4;
            this.HighlightAllButton.Text = "HighlightAll";
            this.HighlightAllButton.UseVisualStyleBackColor = true;
            this.HighlightAllButton.Click += new System.EventHandler(this.HighlightAllButton_Click);
            // 
            // RecordButton
            // 
            this.RecordButton.Location = new System.Drawing.Point(583, 47);
            this.RecordButton.Name = "RecordButton";
            this.RecordButton.Size = new System.Drawing.Size(205, 23);
            this.RecordButton.TabIndex = 5;
            this.RecordButton.Text = "Record";
            this.RecordButton.UseVisualStyleBackColor = true;
            this.RecordButton.Click += new System.EventHandler(this.RecordButton_Click);
            // 
            // LoadButton
            // 
            this.LoadButton.Location = new System.Drawing.Point(583, 12);
            this.LoadButton.Name = "LoadButton";
            this.LoadButton.Size = new System.Drawing.Size(205, 23);
            this.LoadButton.TabIndex = 6;
            this.LoadButton.Text = "Load";
            this.LoadButton.UseVisualStyleBackColor = true;
            this.LoadButton.Click += new System.EventHandler(this.LoadButton_Click);
            // 
            // SpiritTypingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.LoadButton);
            this.Controls.Add(this.RecordButton);
            this.Controls.Add(this.HighlightAllButton);
            this.Controls.Add(this.ExecuteNextButton);
            this.Controls.Add(this.Playbutton);
            this.Controls.Add(this.SaveButton);
            this.Controls.Add(this.TextEntryWindow);
            this.Name = "SpiritTypingForm";
            this.Text = "Form1";
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.Button LoadButton;

        private System.Windows.Forms.Button RecordButton;

        private System.Windows.Forms.Button HighlightAllButton;

        private System.Windows.Forms.Button ExecuteNextButton;

        private System.Windows.Forms.Button Playbutton;

        private System.Windows.Forms.Button SaveButton;
        
        private System.Windows.Forms.RichTextBox TextEntryWindow;

        #endregion
    }
}