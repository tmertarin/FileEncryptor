using System;
using System.Drawing;
using System.IO;
using System.Security.Cryptography;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FileEncryptor
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            txtFilePath.AllowDrop = true;
            txtFilePath.DragEnter += TxtFilePath_DragEnter;
            txtFilePath.DragDrop += TxtFilePath_DragDrop;
            txtPassword.UseSystemPasswordChar = true;
        }

        private void BtnBrowse_Click(object? sender, EventArgs e)
        {
            DialogResult r = openFileDialog1.ShowDialog();
            if (r == DialogResult.OK)
            {
                string p = openFileDialog1.FileName;
                if (!string.IsNullOrEmpty(p))
                {
                    SetFilePath(p);
                }
            }
        }

        private void TxtFilePath_DragEnter(object? sender, DragEventArgs e)
        {
            DragDropEffects effect = DragDropEffects.None;
            if (e.Data != null)
            {
                bool ok = e.Data.GetDataPresent(DataFormats.FileDrop);
                effect = ok ? DragDropEffects.Copy : DragDropEffects.None;
            }
            e.Effect = effect;
        }

        private void TxtFilePath_DragDrop(object? sender, DragEventArgs e)
        {
            if (e.Data is not null && e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                if (e.Data.GetData(DataFormats.FileDrop) is string[] files && files.Length > 0)
                {
                    string first = files[0];
                    SetFilePath(first);

                    if (files.Length > 1)
                    {
                        LogWarning("Multiple files detected, only the first one will be used.");
                    }
                }
            }
        }

        private void SetFilePath(string path)
        {
            txtFilePath.Text = path;
            string msg = string.Concat("File selected: ", path);
            LogInfo(msg);
        }

        private async void BtnEncrypt_Click(object? sender, EventArgs e)
        {
            bool ok = PreCheck(encrypting: true);
            if (!ok) return;

            string path = txtFilePath.Text;
            string pass = txtPassword.Text;

            await RunCryptoOperation(
                () => AESHelper.EncryptFile(path, pass),
                "Encryption completed."
            );
        }

        private async void BtnDecrypt_Click(object? sender, EventArgs e)
        {
            bool ok = PreCheck(encrypting: false);
            if (!ok) return;

            string path = txtFilePath.Text;
            string pass = txtPassword.Text;

            await RunCryptoOperation(
                () => AESHelper.DecryptFile(path, pass),
                "Decryption completed."
            );
        }

        private async Task RunCryptoOperation(Action action, string successMessage)
        {
            try
            {
                ToggleControls(false);

                Task t = Task.Run(action);
                await t.ConfigureAwait(true);

                if (t.IsCompletedSuccessfully)
                {
                    LogSuccess(successMessage);
                }
            }
            catch (CryptographicException)
            {
                LogError("Incorrect password or corrupted file.");
            }
            catch (InvalidDataException ide)
            {
                string m = string.Concat("Invalid file format: ", ide.Message);
                LogError(m);
            }
            catch (Exception ex)
            {
                LogError("Unexpected error: " + ex.Message);
            }
            finally
            {
                ClearPassword();
                ToggleControls(true);
            }
        }

        private bool PreCheck(bool encrypting)
        {
            bool emptyPath = string.IsNullOrWhiteSpace(txtFilePath.Text);
            bool emptyPw = string.IsNullOrWhiteSpace(txtPassword.Text);

            if (emptyPath || emptyPw)
            {
                LogWarning("File path and password are required.");
                return false;
            }

            bool alreadyEncrypted = encrypting && txtFilePath.Text.EndsWith(".aes", StringComparison.OrdinalIgnoreCase);

            if (alreadyEncrypted)
            {
                LogWarning("An already encrypted file cannot be re-encrypted.");
                return false;
            }

            bool strong = IsPasswordStrong(txtPassword.Text);
            if (!strong)
            {
                LogWarning("Password is too weak. At least 10 chars, letter + digit recommended.");
                return false;
            }
            return true;
        }

        private static bool IsPasswordStrong(string pw)
        {
            int len = pw.Length;
            if (len < 10) return false;

            bool hasLetter = false;
            bool hasDigit = false;

            for (int i = 0; i < pw.Length; i++)
            {
                char c = pw[i];
                if (!hasLetter && char.IsLetter(c)) hasLetter = true;
                if (!hasDigit && char.IsDigit(c)) hasDigit = true;
            }

            return hasLetter && hasDigit;
        }

        private void ToggleControls(bool enabled)
        {
            Button[] buttons = { btnEncrypt, btnDecrypt, btnBrowse };
            for (int i = 0; i < buttons.Length; i++)
            {
                buttons[i].Enabled = enabled;
            }
        }

        private void ClearPassword()
        {
            if (!string.IsNullOrEmpty(txtPassword.Text))
            {
                txtPassword.Text = string.Empty;
            }
        }

        private void LogInfo(string msg)
        {
            AppendLog(msg, Color.DodgerBlue, "[INFO]");
        }

        private void LogSuccess(string msg)
        {
            AppendLog(msg, Color.LimeGreen, "[SUCCESS]");
        }

        private void LogWarning(string msg)
        {
            AppendLog(msg, Color.Orange, "[WARNING]");
        }

        private void LogError(string msg)
        {
            AppendLog(msg, Color.Red, "[ERROR]");
        }

        private void AppendLog(string message, Color color, string prefix)
        {
            int start = richLog.TextLength;
            richLog.SelectionStart = start;
            richLog.SelectionColor = color;

            string line = string.Concat(prefix, " ", message, Environment.NewLine);
            richLog.AppendText(line);

            richLog.SelectionColor = richLog.ForeColor;
            richLog.ScrollToCaret();
        }

        private void TxtFilePath_DoubleClick(object? sender, EventArgs e)
        {
            DialogResult r = openFileDialog1.ShowDialog();
            if (r == DialogResult.OK)
            {
                string p = openFileDialog1.FileName;
                SetFilePath(p);
            }
        }
    }
}
