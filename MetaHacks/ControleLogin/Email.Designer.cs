namespace MetaHacks
{
    partial class Email
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Email));
            this.mpEmail = new MetroFramework.Controls.MetroTile();
            this.txtVal = new MetroFramework.Controls.MetroTextBox();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.SuspendLayout();
            // 
            // mpEmail
            // 
            this.mpEmail.ActiveControl = null;
            this.mpEmail.BackColor = System.Drawing.Color.SlateBlue;
            this.mpEmail.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.mpEmail.Location = new System.Drawing.Point(172, 230);
            this.mpEmail.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.mpEmail.Name = "mpEmail";
            this.mpEmail.Size = new System.Drawing.Size(187, 82);
            this.mpEmail.TabIndex = 0;
            this.mpEmail.Text = "Validar Email";
            this.mpEmail.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.mpEmail.UseCustomBackColor = true;
            this.mpEmail.UseCustomForeColor = true;
            this.mpEmail.UseSelectable = true;
            this.mpEmail.Click += new System.EventHandler(this.mpEmail_Click);
            // 
            // txtVal
            // 
            // 
            // 
            // 
            this.txtVal.CustomButton.Image = null;
            this.txtVal.CustomButton.Location = new System.Drawing.Point(161, 1);
            this.txtVal.CustomButton.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.txtVal.CustomButton.Name = "";
            this.txtVal.CustomButton.Size = new System.Drawing.Size(25, 25);
            this.txtVal.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.txtVal.CustomButton.TabIndex = 1;
            this.txtVal.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.txtVal.CustomButton.UseSelectable = true;
            this.txtVal.CustomButton.Visible = false;
            this.txtVal.FontSize = MetroFramework.MetroTextBoxSize.Tall;
            this.txtVal.ForeColor = System.Drawing.Color.SlateBlue;
            this.txtVal.Lines = new string[] {
        ".   .   .   .   .   . "};
            this.txtVal.Location = new System.Drawing.Point(172, 172);
            this.txtVal.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.txtVal.MaxLength = 32767;
            this.txtVal.Name = "txtVal";
            this.txtVal.PasswordChar = '\0';
            this.txtVal.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtVal.SelectedText = "";
            this.txtVal.SelectionLength = 0;
            this.txtVal.SelectionStart = 0;
            this.txtVal.ShortcutsEnabled = true;
            this.txtVal.Size = new System.Drawing.Size(187, 27);
            this.txtVal.TabIndex = 1;
            this.txtVal.Text = ".   .   .   .   .   . ";
            this.txtVal.UseCustomForeColor = true;
            this.txtVal.UseSelectable = true;
            this.txtVal.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.txtVal.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            this.txtVal.Enter += new System.EventHandler(this.txtVal_Enter);
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.LinkColor = System.Drawing.Color.DarkGray;
            this.linkLabel1.Location = new System.Drawing.Point(172, 319);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(160, 15);
            this.linkLabel1.TabIndex = 2;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "Não recebi meu código.";
            this.linkLabel1.VisitedLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // Email
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(572, 462);
            this.Controls.Add(this.linkLabel1);
            this.Controls.Add(this.txtVal);
            this.Controls.Add(this.mpEmail);
            this.Font = new System.Drawing.Font("Arial Rounded MT Bold", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.MaximizeBox = false;
            this.Name = "Email";
            this.Padding = new System.Windows.Forms.Padding(27, 69, 27, 23);
            this.Resizable = false;
            this.Style = MetroFramework.MetroColorStyle.Purple;
            this.Text = "Validação de E-mail";
            this.TextAlign = MetroFramework.Forms.MetroFormTextAlign.Center;
            this.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.TopMost = true;
            this.Load += new System.EventHandler(this.Email_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MetroFramework.Controls.MetroTile mpEmail;
        private MetroFramework.Controls.MetroTextBox txtVal;
        private System.Windows.Forms.LinkLabel linkLabel1;
    }
}