namespace WindowsFormsApplication1
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
            this.lstNumber = new System.Windows.Forms.ListBox();
            this.cmdGenerate = new System.Windows.Forms.Button();
            this.pgrOperation = new System.Windows.Forms.ProgressBar();
            this.SuspendLayout();
            // 
            // lstNumber
            // 
            this.lstNumber.FormattingEnabled = true;
            this.lstNumber.Location = new System.Drawing.Point(12, 30);
            this.lstNumber.Name = "lstNumber";
            this.lstNumber.Size = new System.Drawing.Size(263, 160);
            this.lstNumber.TabIndex = 0;
            // 
            // cmdGenerate
            // 
            this.cmdGenerate.Location = new System.Drawing.Point(374, 129);
            this.cmdGenerate.Name = "cmdGenerate";
            this.cmdGenerate.Size = new System.Drawing.Size(75, 23);
            this.cmdGenerate.TabIndex = 1;
            this.cmdGenerate.Text = "button1";
            this.cmdGenerate.UseVisualStyleBackColor = true;
            this.cmdGenerate.Click += new System.EventHandler(this.cmdGenerate_Click);
            // 
            // pgrOperation
            // 
            this.pgrOperation.Location = new System.Drawing.Point(23, 196);
            this.pgrOperation.Name = "pgrOperation";
            this.pgrOperation.Size = new System.Drawing.Size(483, 23);
            this.pgrOperation.TabIndex = 2;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(518, 258);
            this.Controls.Add(this.pgrOperation);
            this.Controls.Add(this.cmdGenerate);
            this.Controls.Add(this.lstNumber);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox lstNumber;
        private System.Windows.Forms.Button cmdGenerate;
        private System.Windows.Forms.ProgressBar pgrOperation;
    }
}

