namespace SimpleForms
{
    partial class SF_Error
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
            this.errorTxt = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // errorTxt
            // 
            this.errorTxt.AutoSize = true;
            this.errorTxt.Location = new System.Drawing.Point(12, 9);
            this.errorTxt.Name = "errorTxt";
            this.errorTxt.Size = new System.Drawing.Size(45, 13);
            this.errorTxt.TabIndex = 1;
            this.errorTxt.Text = "[errortxt]";
            // 
            // SF_Error
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(338, 117);
            this.Controls.Add(this.errorTxt);
            this.Name = "SF_Error";
            this.Text = "SF_Error";
            this.Load += new System.EventHandler(this.SF_Error_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label errorTxt;
    }
}