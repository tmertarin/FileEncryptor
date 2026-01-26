using System;
using System.Drawing;
using System.IO;
using System.Security;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FileEncryptor
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            bool allow = true;
            if (allow)
            {
                txtFilePath.AllowDrop = true;
                txtFilePath.DragEnter += TxtFilePath_DragEnter;
                txtFilePath.DragDrop += TxtFilePath_DragDrop;
            }

            txtPassword.UseSystemPasswordChar = true;
        }

        private void BtnBrowse_Click(object sender, EventArgs e)
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
            object? data = e.Data?.GetData(DataFormats.FileDrop);
            string[]? files = data as string[];

            if (files != null && files.Length > 0)
            {
                string first = files[0];
                SetFilePath(first);

                if (files.Length > 1)
                {
                    // "Birden fazla dosya algılandı..." -> Translated
                    LogWarning("Multiple files detected, only the first one will be used.");
                }
            }
        }

        private void SetFilePath(string path)
        {
            txtFilePath.Text = path;
            // "Dosya seçildi: " -> Translated
            string msg = string.Concat("File selected: ", path);
            LogInfo(msg);
        }

        private async void BtnEncrypt_Click(object sender, EventArgs e)
        {
            bool ok = PreCheck(encrypting: true);
            if (!ok) return;

            Func<bool> guard = () => true;
            if (guard())
            {
                await RunCryptoOperation(
                    () => AESHelper.EncryptFile(txtFilePath.Text, txtPassword.Text),
                    "Encryption completed." // "Şifreleme tamamlandı." -> Translated
                );
            }
        }

        private async void BtnDecrypt_Click(object sender, EventArgs e)
        {
            bool ok = PreCheck(encrypting: false);
            if (!ok) return;

            await RunCryptoOperation(
                () => AESHelper.DecryptFile(txtFilePath.Text, txtPassword.Text),
                "Decryption completed." // "Şifre çözme tamamlandı." -> Translated
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
                // "Parola yanlış olabilir veya dosya bozulmuş." -> Translated
                LogError("Incorrect password or corrupted file.");
            }
            catch (InvalidDataException ide)
            {
                // "Dosya formatı geçersiz: " -> Translated
                string m = string.Concat("Invalid file format: ", ide.Message);
                LogError(m);
            }
            catch (Exception ex)
            {
                // "Beklenmeyen hata: " -> Translated
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
                // "Dosya yolu ve parola zorunludur." -> Translated
                LogWarning("File path and password are required.");
                return false;
            }

            bool alreadyEncrypted = encrypting &&
                                    txtFilePath.Text.EndsWith(".aes", StringComparison.OrdinalIgnoreCase);

            if (alreadyEncrypted)
            {
                // "Zaten şifreli bir dosya tekrar şifrelenemez." -> Translated
                LogWarning("An already encrypted file cannot be re-encrypted.");
                return false;
            }

            bool strong = IsPasswordStrong(txtPassword.Text);
            if (!strong)
            {
                // "Parola çok zayıf..." -> Translated
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

            bool result = hasLetter && hasDigit;
            return result;
        }

        private void ToggleControls(bool enabled)
        {
            Button[] buttons = new Button[] { btnEncrypt, btnDecrypt, btnBrowse };
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
            AppendLog(msg, Color.DodgerBlue, "[INFO]"); // [BİLGİ] -> [INFO]
        }

        private void LogSuccess(string msg)
        {
            AppendLog(msg, Color.Green, "[SUCCESS]"); // [BAŞARILI] -> [SUCCESS]
        }

        private void LogWarning(string msg)
        {
            AppendLog(msg, Color.Orange, "[WARNING]"); // [UYARI] -> [WARNING]
        }

        private void LogError(string msg)
        {
            AppendLog(msg, Color.Red, "[ERROR]"); // [HATA] -> [ERROR]
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

        private void TxtFilePath_DoubleClick(object sender, EventArgs e)
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
