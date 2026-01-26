namespace FileEncryptor
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.txtFilePath = new System.Windows.Forms.TextBox();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.labelFile = new System.Windows.Forms.Label();
            this.labelPassword = new System.Windows.Forms.Label();
            this.btnEncrypt = new System.Windows.Forms.Button();
            this.btnDecrypt = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.richLog = new System.Windows.Forms.RichTextBox();
            this.labelLog = new System.Windows.Forms.Label();
            this.lblDragDrop = new System.Windows.Forms.Label();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.labelVersion = new System.Windows.Forms.Label();
            this.SuspendLayout();

            this.txtFilePath.AllowDrop = true;
            this.txtFilePath.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.txtFilePath.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtFilePath.Font = new System.Drawing.Font("Consolas", 11F);
            this.txtFilePath.ForeColor = System.Drawing.Color.LimeGreen;
            this.txtFilePath.Location = new System.Drawing.Point(25, 45);
            this.txtFilePath.Name = "txtFilePath";
            this.txtFilePath.ReadOnly = true;
            this.txtFilePath.Size = new System.Drawing.Size(324, 25);
            this.txtFilePath.TabIndex = 0;
            this.txtFilePath.DragDrop += new System.Windows.Forms.DragEventHandler(this.TxtFilePath_DragDrop);
            this.txtFilePath.DragEnter += new System.Windows.Forms.DragEventHandler(this.TxtFilePath_DragEnter);
            this.txtFilePath.DoubleClick += new System.EventHandler(this.TxtFilePath_DoubleClick);

            this.btnBrowse.BackColor = System.Drawing.Color.SeaGreen;
            this.btnBrowse.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnBrowse.FlatAppearance.BorderSize = 0;
            this.btnBrowse.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBrowse.Font = new System.Drawing.Font("Consolas", 10F, System.Drawing.FontStyle.Bold);
            this.btnBrowse.ForeColor = System.Drawing.Color.White;
            this.btnBrowse.Location = new System.Drawing.Point(355, 44);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(60, 27);
            this.btnBrowse.TabIndex = 1;
            this.btnBrowse.Text = "...";
            this.btnBrowse.UseVisualStyleBackColor = false;
            this.btnBrowse.Click += new System.EventHandler(this.BtnBrowse_Click);

            this.txtPassword.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.txtPassword.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPassword.Font = new System.Drawing.Font("Consolas", 11F);
            this.txtPassword.ForeColor = System.Drawing.Color.LimeGreen;
            this.txtPassword.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.txtPassword.Location = new System.Drawing.Point(25, 120);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '●';
            this.txtPassword.ShortcutsEnabled = false;
            this.txtPassword.Size = new System.Drawing.Size(390, 25);
            this.txtPassword.TabIndex = 3;
            this.txtPassword.UseSystemPasswordChar = true;

            this.labelFile.AutoSize = true;
            this.labelFile.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Bold);
            this.labelFile.ForeColor = System.Drawing.Color.Silver;
            this.labelFile.Location = new System.Drawing.Point(25, 20);
            this.labelFile.Name = "labelFile";
            this.labelFile.Size = new System.Drawing.Size(70, 14);
            this.labelFile.TabIndex = 2;
            this.labelFile.Text = "FILE PATH";

            this.labelPassword.AutoSize = true;
            this.labelPassword.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Bold);
            this.labelPassword.ForeColor = System.Drawing.Color.Silver;
            this.labelPassword.Location = new System.Drawing.Point(25, 95);
            this.labelPassword.Name = "labelPassword";
            this.labelPassword.Size = new System.Drawing.Size(63, 14);
            this.labelPassword.TabIndex = 4;
            this.labelPassword.Text = "PASSWORD";

            this.btnEncrypt.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(167)))), ((int)(((byte)(69)))));
            this.btnEncrypt.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnEncrypt.FlatAppearance.BorderSize = 0;
            this.btnEncrypt.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEncrypt.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Bold);
            this.btnEncrypt.ForeColor = System.Drawing.Color.White;
            this.btnEncrypt.Location = new System.Drawing.Point(25, 165);
            this.btnEncrypt.Name = "btnEncrypt";
            this.btnEncrypt.Size = new System.Drawing.Size(190, 45);
            this.btnEncrypt.TabIndex = 5;
            this.btnEncrypt.Text = "ENCRYPT";
            this.btnEncrypt.UseVisualStyleBackColor = false;
            this.btnEncrypt.Click += new System.EventHandler(this.BtnEncrypt_Click);

            this.btnDecrypt.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(123)))), ((int)(((byte)(255)))));
            this.btnDecrypt.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDecrypt.FlatAppearance.BorderSize = 0;
            this.btnDecrypt.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDecrypt.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Bold);
            this.btnDecrypt.ForeColor = System.Drawing.Color.White;
            this.btnDecrypt.Location = new System.Drawing.Point(225, 165);
            this.btnDecrypt.Name = "btnDecrypt";
            this.btnDecrypt.Size = new System.Drawing.Size(190, 45);
            this.btnDecrypt.TabIndex = 6;
            this.btnDecrypt.Text = "DECRYPT";
            this.btnDecrypt.UseVisualStyleBackColor = false;
            this.btnDecrypt.Click += new System.EventHandler(this.BtnDecrypt_Click);

            this.openFileDialog1.FileName = "openFileDialog1";

            this.richLog.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(15)))), ((int)(((byte)(15)))));
            this.richLog.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.richLog.Font = new System.Drawing.Font("Consolas", 9.5F);
            this.richLog.ForeColor = System.Drawing.Color.LimeGreen;
            this.richLog.Location = new System.Drawing.Point(25, 245);
            this.richLog.Name = "richLog";
            this.richLog.ReadOnly = true;
            this.richLog.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.richLog.Size = new System.Drawing.Size(390, 165);
            this.richLog.TabIndex = 7;
            this.richLog.Text = "";

            this.labelLog.AutoSize = true;
            this.labelLog.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Bold);
            this.labelLog.ForeColor = System.Drawing.Color.Silver;
            this.labelLog.Location = new System.Drawing.Point(25, 225);
            this.labelLog.Name = "labelLog";
            this.labelLog.Size = new System.Drawing.Size(84, 14);
            this.labelLog.TabIndex = 8;
            this.labelLog.Text = "SYSTEM LOGS";

            this.lblDragDrop.AutoSize = true;
            this.lblDragDrop.Font = new System.Drawing.Font("Consolas", 8F, System.Drawing.FontStyle.Italic);
            this.lblDragDrop.ForeColor = System.Drawing.Color.DimGray;
            this.lblDragDrop.Location = new System.Drawing.Point(25, 75);
            this.lblDragDrop.Name = "lblDragDrop";
            this.lblDragDrop.Size = new System.Drawing.Size(187, 13);
            this.lblDragDrop.TabIndex = 9;
            this.lblDragDrop.Text = "* Drag and drop file supported";
            this.lblDragDrop.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;

            this.labelVersion.AutoSize = true;
            this.labelVersion.Font = new System.Drawing.Font("Consolas", 8F);
            this.labelVersion.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.labelVersion.Location = new System.Drawing.Point(375, 418);
            this.labelVersion.Name = "labelVersion";
            this.labelVersion.Size = new System.Drawing.Size(43, 13);
            this.labelVersion.TabIndex = 10;
            this.labelVersion.Text = "v1.0.0";
            this.labelVersion.TextAlign = System.Drawing.ContentAlignment.BottomRight;

            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(12)))), ((int)(((byte)(12)))));
            this.ClientSize = new System.Drawing.Size(440, 440);
            this.Controls.Add(this.labelVersion);
            this.Controls.Add(this.lblDragDrop);
            this.Controls.Add(this.labelLog);
            this.Controls.Add(this.richLog);
            this.Controls.Add(this.btnDecrypt);
            this.Controls.Add(this.btnEncrypt);
            this.Controls.Add(this.labelPassword);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.labelFile);
            this.Controls.Add(this.btnBrowse);
            this.Controls.Add(this.txtFilePath);
            this.Font = new System.Drawing.Font("Consolas", 10F);
            this.ForeColor = System.Drawing.Color.White;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FileCryptor";
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.TextBox txtFilePath;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Label labelFile;
        private System.Windows.Forms.Label labelPassword;
        private System.Windows.Forms.Button btnEncrypt;
        private System.Windows.Forms.Button btnDecrypt;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.RichTextBox richLog;
        private System.Windows.Forms.Label labelLog;
        private System.Windows.Forms.Label lblDragDrop;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.Label labelVersion;
    }
}
