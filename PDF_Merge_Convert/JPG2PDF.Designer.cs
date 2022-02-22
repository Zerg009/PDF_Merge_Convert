namespace PDF_Merge_Convert
{
    partial class JPG2PDF
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
            this.BrowseBtn = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // BrowseBtn
            // 
            this.BrowseBtn.Location = new System.Drawing.Point(93, 31);
            this.BrowseBtn.Name = "BrowseBtn";
            this.BrowseBtn.Size = new System.Drawing.Size(133, 44);
            this.BrowseBtn.TabIndex = 1;
            this.BrowseBtn.Text = "Browse";
            this.BrowseBtn.UseVisualStyleBackColor = true;
            this.BrowseBtn.Click += new System.EventHandler(this.BrowseBtn_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(93, 96);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(133, 45);
            this.button1.TabIndex = 2;
            this.button1.Text = "Merge";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.Button1_Click);
            // 
            // JPG2PDF
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(296, 178);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.BrowseBtn);
            this.Name = "JPG2PDF";
            this.Text = "JPG to PDF";
            this.Load += new System.EventHandler(this.JPG2PDF_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button BrowseBtn;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button button1;
    }
}