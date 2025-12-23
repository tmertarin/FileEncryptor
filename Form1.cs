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

            txtFilePath.AllowDrop = true;
            txtFilePath.DragEnter += TxtFilePath_DragEnter;
            txtFilePath.DragDrop += TxtFilePath_DragDrop;

            txtPassword.UseSystemPasswordChar = true;
        }

        /* ================= FILE PICK ================= */

        private void BtnBrowse_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
                SetFilePath(openFileDialog1.FileName);
        }

        private void TxtFilePath_DragEnter(object? sender, DragEventArgs e)
        {
            e.Effect = (e.Data != null && e.Data.GetDataPresent(DataFormats.FileDrop))
                ? DragDropEffects.Copy
                : DragDropEffects.None;
        }

        private void TxtFilePath_DragDrop(object? sender, DragEventArgs e)
        {
            var files = e.Data?.GetData(DataFormats.FileDrop) as string[];
            if (files is { Length: > 0 })
            {
                SetFilePath(files[0]);
                if (files.Length > 1)
                    LogWarning("Birden fazla dosya algýlandý, sadece ilki kullanýlacak.");
            }
        }

        private void SetFilePath(string path)
        {
            txtFilePath.Text = path;
            LogInfo($"Dosya seçildi: {path}");
        }

        /* ================= BUTTONS ================= */

        private async void BtnEncrypt_Click(object sender, EventArgs e)
        {
            if (!PreCheck(encrypting: true)) return;

            await RunCryptoOperation(
                () => AESHelper.EncryptFile(txtFilePath.Text, txtPassword.Text),
                "Þifreleme tamamlandý."
            );
        }

        private async void BtnDecrypt_Click(object sender, EventArgs e)
        {
            if (!PreCheck(encrypting: false)) return;

            await RunCryptoOperation(
                () => AESHelper.DecryptFile(txtFilePath.Text, txtPassword.Text),
                "Þifre çözme tamamlandý."
            );
        }

        /* ================= CORE LOGIC ================= */

        private async Task RunCryptoOperation(Action action, string successMessage)
        {
            try
            {
                ToggleControls(false);

                await Task.Run(action);

                LogSuccess(successMessage);
            }
            catch (CryptographicException)
            {
                LogError("Parola yanlýþ olabilir veya dosya bozulmuþ.");
            }
            catch (InvalidDataException ide)
            {
                LogError("Dosya formatý geçersiz: " + ide.Message);
            }
            catch (Exception ex)
            {
                LogError("Beklenmeyen hata: " + ex.Message);
            }
            finally
            {
                ClearPassword();
                ToggleControls(true);
            }
        }

        /* ================= VALIDATION ================= */

        private bool PreCheck(bool encrypting)
        {
            if (string.IsNullOrWhiteSpace(txtFilePath.Text) ||
                string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                LogWarning("Dosya yolu ve parola zorunludur.");
                return false;
            }

            if (encrypting && txtFilePath.Text.EndsWith(".aes", StringComparison.OrdinalIgnoreCase))
            {
                LogWarning("Zaten þifreli bir dosya tekrar þifrelenemez.");
                return false;
            }

            if (!IsPasswordStrong(txtPassword.Text))
            {
                LogWarning("Parola çok zayýf. En az 10 karakter, harf + sayý önerilir.");
                return false;
            }

            return true;
        }

        private static bool IsPasswordStrong(string pw)
        {
            if (pw.Length < 10) return false;

            bool hasLetter = false, hasDigit = false;
            foreach (char c in pw)
            {
                if (char.IsLetter(c)) hasLetter = true;
                if (char.IsDigit(c)) hasDigit = true;
            }
            return hasLetter && hasDigit;
        }

        /* ================= UI HELPERS ================= */

        private void ToggleControls(bool enabled)
        {
            btnEncrypt.Enabled = enabled;
            btnDecrypt.Enabled = enabled;
            btnBrowse.Enabled = enabled;
        }

        private void ClearPassword()
        {
            txtPassword.Text = string.Empty;
        }

        private void LogInfo(string msg) => AppendLog(msg, Color.DodgerBlue, "[BÝLGÝ]");
        private void LogSuccess(string msg) => AppendLog(msg, Color.Green, "[BAÞARILI]");
        private void LogWarning(string msg) => AppendLog(msg, Color.Orange, "[UYARI]");
        private void LogError(string msg) => AppendLog(msg, Color.Red, "[HATA]");

        private void AppendLog(string message, Color color, string prefix)
        {
            richLog.SelectionStart = richLog.TextLength;
            richLog.SelectionColor = color;
            richLog.AppendText($"{prefix} {message}\n");
            richLog.SelectionColor = richLog.ForeColor;
            richLog.ScrollToCaret();
        }

        private void TxtFilePath_DoubleClick(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
                SetFilePath(openFileDialog1.FileName);
        }
    }
}
