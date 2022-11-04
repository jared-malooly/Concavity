namespace ConcavityDemo
{
    partial class MainForm
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
            this.PictureBox = new System.Windows.Forms.PictureBox();
            this.trackBar = new System.Windows.Forms.TrackBar();
            this.kLbl = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar)).BeginInit();
            this.SuspendLayout();
            // 
            // PictureBox
            // 
            this.PictureBox.Location = new System.Drawing.Point(0, 36);
            this.PictureBox.Name = "PictureBox";
            this.PictureBox.Size = new System.Drawing.Size(671, 452);
            this.PictureBox.TabIndex = 0;
            this.PictureBox.TabStop = false;
            this.PictureBox.MouseDown += new System.Windows.Forms.MouseEventHandler(this.PictureBox_MouseDown);
            this.PictureBox.MouseMove += new System.Windows.Forms.MouseEventHandler(this.PictureBox_MouseDown);
            // 
            // trackBar
            // 
            this.trackBar.AutoSize = false;
            this.trackBar.Location = new System.Drawing.Point(0, 3);
            this.trackBar.Maximum = 500;
            this.trackBar.Minimum = 1;
            this.trackBar.Name = "trackBar";
            this.trackBar.Size = new System.Drawing.Size(587, 27);
            this.trackBar.TabIndex = 1;
            this.trackBar.Value = 50;
            this.trackBar.Scroll += new System.EventHandler(this.trackBar_Scroll);
            // 
            // kLbl
            // 
            this.kLbl.AutoSize = true;
            this.kLbl.Location = new System.Drawing.Point(593, 9);
            this.kLbl.Name = "kLbl";
            this.kLbl.Size = new System.Drawing.Size(20, 13);
            this.kLbl.TabIndex = 2;
            this.kLbl.Text = "K: ";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(671, 488);
            this.Controls.Add(this.kLbl);
            this.Controls.Add(this.trackBar);
            this.Controls.Add(this.PictureBox);
            this.MaximumSize = new System.Drawing.Size(687, 527);
            this.MinimumSize = new System.Drawing.Size(687, 527);
            this.Name = "MainForm";
            this.Text = "Concavity Demo";
            this.Load += new System.EventHandler(this.MainForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox PictureBox;
        private System.Windows.Forms.TrackBar trackBar;
        private System.Windows.Forms.Label kLbl;
    }
}

