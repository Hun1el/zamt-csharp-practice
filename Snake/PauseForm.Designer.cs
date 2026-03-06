namespace Snake
{
    partial class PauseForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Label pauseMessageLabel;

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
            this.pauseMessageLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // pauseMessageLabel
            // 
            this.pauseMessageLabel.AutoSize = true;
            this.pauseMessageLabel.Font = new System.Drawing.Font("Arial", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.pauseMessageLabel.ForeColor = System.Drawing.Color.White;
            this.pauseMessageLabel.Location = new System.Drawing.Point(95, 40);
            this.pauseMessageLabel.Name = "pauseMessageLabel";
            this.pauseMessageLabel.Size = new System.Drawing.Size(210, 37);
            this.pauseMessageLabel.TabIndex = 0;
            this.pauseMessageLabel.Text = "Game Paused";
            // 
            // PauseForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(400, 200);
            this.Controls.Add(this.pauseMessageLabel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "PauseForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Pause";
            this.BackColor = System.Drawing.Color.Black;
            this.Opacity = 0.7;
            this.KeyPreview = true;
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.PauseForm_KeyDown);
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion
    }
}
