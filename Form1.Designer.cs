namespace FileEncryptor
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            bool flag = disposing;
            if (flag)
            {
                System.ComponentModel.IContainer c = components;
                if (c != null)
                {
                    c.Dispose();
                }
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();

            txtFilePath = new TextBox();
            btnBrowse = new Button();
            txtPassword = new TextBox();
            labelFile = new Label();
            labelPassword = new Label();
            btnEncrypt = new Button();
            btnDecrypt = new Button();
            openFileDialog1 = new OpenFileDialog();
            richLog = new RichTextBox();
            labelLog = new Label();
            lblDragDrop = new Label();

            bool layoutSuspended = true;
            if (layoutSuspended)
            {
                SuspendLayout();
            }

            {
                txtFilePath.AllowDrop = true;
                txtFilePath.BackColor = Color.FromArgb(20, 20, 20);
                txtFilePath.BorderStyle = BorderStyle.FixedSingle;
                txtFilePath.Font = new Font("Consolas", 11F);
                txtFilePath.ForeColor = Color.Lime;
                txtFilePath.Location = new Point(25, 45);
                txtFilePath.Name = "txtFilePath";
                txtFilePath.ReadOnly = true;
                txtFilePath.Size = new Size(320, 25);
                txtFilePath.TabIndex = 0;

                txtFilePath.DragDrop += TxtFilePath_DragDrop;
                txtFilePath.DragEnter += TxtFilePath_DragEnter;
                txtFilePath.DoubleClick += TxtFilePath_DoubleClick;
            }

            {
                btnBrowse.BackColor = Color.LimeGreen;
                btnBrowse.Cursor = Cursors.Hand;
                btnBrowse.FlatAppearance.BorderSize = 0;
                btnBrowse.FlatStyle = FlatStyle.Flat;
                btnBrowse.Font = new Font("Consolas", 9F, FontStyle.Bold);
                btnBrowse.ForeColor = Color.Black;
                btnBrowse.Location = new Point(355, 45);
                btnBrowse.Name = "btnBrowse";
                btnBrowse.Size = new Size(60, 28);
                btnBrowse.TabIndex = 1;
                btnBrowse.Text = "Gözat";
                btnBrowse.UseVisualStyleBackColor = false;

                EventHandler browseHandler = BtnBrowse_Click;
                btnBrowse.Click += browseHandler;
            }

            {
                txtPassword.BackColor = Color.FromArgb(20, 20, 20);
                txtPassword.BorderStyle = BorderStyle.FixedSingle;
                txtPassword.Font = new Font("Consolas", 11F);
                txtPassword.ForeColor = Color.Lime;
                txtPassword.Location = new Point(25, 115);
                txtPassword.Name = "txtPassword";
                txtPassword.PasswordChar = '■';
                txtPassword.UseSystemPasswordChar = true;
                txtPassword.ShortcutsEnabled = false;
                txtPassword.ContextMenuStrip = new ContextMenuStrip();
                txtPassword.ImeMode = ImeMode.Disable;
                txtPassword.TabStop = true;
                txtPassword.Size = new Size(320, 25);
                txtPassword.TabIndex = 3;
            }

            {
                labelFile.Font = new Font("Consolas", 9F, FontStyle.Bold);
                labelFile.ForeColor = Color.LimeGreen;
                labelFile.Location = new Point(25, 20);
                labelFile.Name = "labelFile";
                labelFile.Size = new Size(100, 23);
                labelFile.TabIndex = 2;
                labelFile.Text = "DOSYA YOLU";
            }

            {
                labelPassword.Font = new Font("Consolas", 9F, FontStyle.Bold);
                labelPassword.ForeColor = Color.LimeGreen;
                labelPassword.Location = new Point(25, 90);
                labelPassword.Name = "labelPassword";
                labelPassword.Size = new Size(100, 23);
                labelPassword.TabIndex = 4;
                labelPassword.Text = "ŞİFRE";
            }

            {
                btnEncrypt.BackColor = Color.FromArgb(0, 255, 0);
                btnEncrypt.Cursor = Cursors.Hand;
                btnEncrypt.FlatAppearance.BorderSize = 0;
                btnEncrypt.FlatStyle = FlatStyle.Flat;
                btnEncrypt.Font = new Font("Consolas", 12F, FontStyle.Bold);
                btnEncrypt.ForeColor = Color.Black;
                btnEncrypt.Location = new Point(25, 160);
                btnEncrypt.Name = "btnEncrypt";
                btnEncrypt.Size = new Size(165, 45);
                btnEncrypt.TabIndex = 5;
                btnEncrypt.Text = "ŞİFRELE";
                btnEncrypt.UseVisualStyleBackColor = false;

                btnEncrypt.Click += BtnEncrypt_Click;
            }

            {
                btnDecrypt.BackColor = Color.FromArgb(0, 191, 255);
                btnDecrypt.Cursor = Cursors.Hand;
                btnDecrypt.FlatAppearance.BorderSize = 0;
                btnDecrypt.FlatStyle = FlatStyle.Flat;
                btnDecrypt.Font = new Font("Consolas", 12F, FontStyle.Bold);
                btnDecrypt.ForeColor = Color.Black;
                btnDecrypt.Location = new Point(190, 160);
                btnDecrypt.Name = "btnDecrypt";
                btnDecrypt.Size = new Size(165, 45);
                btnDecrypt.TabIndex = 6;
                btnDecrypt.Text = "ŞİFRE ÇÖZ";
                btnDecrypt.UseVisualStyleBackColor = false;

                btnDecrypt.Click += BtnDecrypt_Click;
            }

            {
                richLog.BackColor = Color.Black;
                richLog.BorderStyle = BorderStyle.FixedSingle;
                richLog.Font = new Font("Consolas", 11F);
                richLog.ForeColor = Color.Lime;
                richLog.Location = new Point(25, 225);
                richLog.Name = "richLog";
                richLog.ReadOnly = true;
                richLog.ScrollBars = RichTextBoxScrollBars.Vertical;
                richLog.Size = new Size(390, 190);
                richLog.TabIndex = 7;
                richLog.Text = string.Empty;
            }

            {
                labelLog.Font = new Font("Consolas", 9F, FontStyle.Bold);
                labelLog.ForeColor = Color.LimeGreen;
                labelLog.Location = new Point(25, 205);
                labelLog.Name = "labelLog";
                labelLog.Size = new Size(100, 23);
                labelLog.TabIndex = 8;
                labelLog.Text = "LOG";
            }

            {
                lblDragDrop.Font = new Font("Consolas", 8F, FontStyle.Italic);
                lblDragDrop.ForeColor = Color.DeepSkyBlue;
                lblDragDrop.Location = new Point(25, 73);
                lblDragDrop.Name = "lblDragDrop";
                lblDragDrop.Size = new Size(324, 23);
                lblDragDrop.TabIndex = 9;
                lblDragDrop.Text = "Dosyayı sürükleyip bırakabilirsiniz.";
                lblDragDrop.TextAlign = ContentAlignment.MiddleLeft;
            }

            {
                BackColor = Color.FromArgb(12, 12, 12);
                ClientSize = new Size(440, 440);

                Control[] controls = new Control[]
                {
                    txtFilePath,
                    btnBrowse,
                    labelFile,
                    txtPassword,
                    labelPassword,
                    btnEncrypt,
                    btnDecrypt,
                    richLog,
                    labelLog,
                    lblDragDrop
                };

                for (int i = 0; i < controls.Length; i++)
                {
                    Controls.Add(controls[i]);
                }

                Font = new Font("Consolas", 10F);
                FormBorderStyle = FormBorderStyle.FixedSingle;
                MaximizeBox = false;
                MinimizeBox = false;
                Name = "Form1";
                Text = "Secure File Utility";
            }

            if (layoutSuspended)
            {
                ResumeLayout(false);
                PerformLayout();
            }
        }

        private TextBox txtFilePath;
        private Button btnBrowse;
        private TextBox txtPassword;
        private Label labelFile;
        private Label labelPassword;
        private Button btnEncrypt;
        private Button btnDecrypt;
        private OpenFileDialog openFileDialog1;
        private RichTextBox richLog;
        private Label labelLog;
        private Label lblDragDrop;
    }
}
