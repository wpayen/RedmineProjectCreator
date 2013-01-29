namespace Redmineprojectcreation
{
    partial class Apikey
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
            this.apiKeyOk = new System.Windows.Forms.Button();
            this.apiKeyOff = new System.Windows.Forms.Button();
            this.apiKeyTextbox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // apiKeyOk
            // 
            this.apiKeyOk.Location = new System.Drawing.Point(115, 37);
            this.apiKeyOk.Name = "apiKeyOk";
            this.apiKeyOk.Size = new System.Drawing.Size(75, 23);
            this.apiKeyOk.TabIndex = 0;
            this.apiKeyOk.Text = "Ok";
            this.apiKeyOk.UseVisualStyleBackColor = true;
            this.apiKeyOk.Click += new System.EventHandler(this.apiKeyOk_Click);
            // 
            // apiKeyOff
            // 
            this.apiKeyOff.Location = new System.Drawing.Point(218, 37);
            this.apiKeyOff.Name = "apiKeyOff";
            this.apiKeyOff.Size = new System.Drawing.Size(75, 23);
            this.apiKeyOff.TabIndex = 1;
            this.apiKeyOff.Text = "Annuler";
            this.apiKeyOff.UseVisualStyleBackColor = true;
            this.apiKeyOff.Click += new System.EventHandler(this.apiKeyOff_Click);
            // 
            // apiKeyTextbox
            // 
            this.apiKeyTextbox.Location = new System.Drawing.Point(-1, 0);
            this.apiKeyTextbox.Name = "apiKeyTextbox";
            this.apiKeyTextbox.Size = new System.Drawing.Size(390, 20);
            this.apiKeyTextbox.TabIndex = 2;
            // 
            // Apikey
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(388, 59);
            this.Controls.Add(this.apiKeyTextbox);
            this.Controls.Add(this.apiKeyOff);
            this.Controls.Add(this.apiKeyOk);
            this.Name = "Apikey";
            this.Text = "Change ApiKey";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.Button apiKeyOk;
        public System.Windows.Forms.Button apiKeyOff;
        public System.Windows.Forms.TextBox apiKeyTextbox;
    }
}