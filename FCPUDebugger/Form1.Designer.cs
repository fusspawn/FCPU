namespace FCPUDebugger
{
    partial class Form1
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
            this.CompileButton = new System.Windows.Forms.Button();
            this.SourceCode = new System.Windows.Forms.RichTextBox();
            this.RegisterListBox = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.StepCPUButton = new System.Windows.Forms.Button();
            this.ResetCPUButton = new System.Windows.Forms.Button();
            this.MemoryDisplay = new System.Windows.Forms.ListBox();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // CompileButton
            // 
            this.CompileButton.Location = new System.Drawing.Point(320, 17);
            this.CompileButton.Name = "CompileButton";
            this.CompileButton.Size = new System.Drawing.Size(75, 23);
            this.CompileButton.TabIndex = 0;
            this.CompileButton.Text = "Compile";
            this.CompileButton.UseVisualStyleBackColor = true;
            this.CompileButton.Click += new System.EventHandler(this.CompileButton_Click);
            // 
            // SourceCode
            // 
            this.SourceCode.Location = new System.Drawing.Point(13, 54);
            this.SourceCode.Name = "SourceCode";
            this.SourceCode.Size = new System.Drawing.Size(389, 539);
            this.SourceCode.TabIndex = 1;
            this.SourceCode.Text = "";
            // 
            // RegisterListBox
            // 
            this.RegisterListBox.FormattingEnabled = true;
            this.RegisterListBox.Location = new System.Drawing.Point(420, 78);
            this.RegisterListBox.Name = "RegisterListBox";
            this.RegisterListBox.Size = new System.Drawing.Size(419, 121);
            this.RegisterListBox.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(422, 57);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(51, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Registers";
            // 
            // StepCPUButton
            // 
            this.StepCPUButton.Location = new System.Drawing.Point(754, 17);
            this.StepCPUButton.Name = "StepCPUButton";
            this.StepCPUButton.Size = new System.Drawing.Size(75, 23);
            this.StepCPUButton.TabIndex = 4;
            this.StepCPUButton.Text = "Execute";
            this.StepCPUButton.UseVisualStyleBackColor = true;
            this.StepCPUButton.Click += new System.EventHandler(this.StepCPUButton_Click);
            // 
            // ResetCPUButton
            // 
            this.ResetCPUButton.Location = new System.Drawing.Point(673, 17);
            this.ResetCPUButton.Name = "ResetCPUButton";
            this.ResetCPUButton.Size = new System.Drawing.Size(75, 23);
            this.ResetCPUButton.TabIndex = 5;
            this.ResetCPUButton.Text = "Reset";
            this.ResetCPUButton.UseVisualStyleBackColor = true;
            this.ResetCPUButton.Click += new System.EventHandler(this.ResetCPUButton_Click);
            // 
            // MemoryDisplay
            // 
            this.MemoryDisplay.FormattingEnabled = true;
            this.MemoryDisplay.Location = new System.Drawing.Point(420, 238);
            this.MemoryDisplay.Name = "MemoryDisplay";
            this.MemoryDisplay.Size = new System.Drawing.Size(419, 355);
            this.MemoryDisplay.TabIndex = 6;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(425, 219);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(44, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "Memory";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(851, 605);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.MemoryDisplay);
            this.Controls.Add(this.ResetCPUButton);
            this.Controls.Add(this.StepCPUButton);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.RegisterListBox);
            this.Controls.Add(this.SourceCode);
            this.Controls.Add(this.CompileButton);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button CompileButton;
        private System.Windows.Forms.RichTextBox SourceCode;
        private System.Windows.Forms.ListBox RegisterListBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button StepCPUButton;
        private System.Windows.Forms.Button ResetCPUButton;
        private System.Windows.Forms.ListBox MemoryDisplay;
        private System.Windows.Forms.Label label2;
    }
}

