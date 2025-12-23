using System;
using System.Threading;
using System.Windows.Forms;

namespace FileEncryptor
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            // UI thread hatalarý
            Application.ThreadException += OnThreadException;

            // UI dýþý (Task / background) hatalar
            AppDomain.CurrentDomain.UnhandledException += OnUnhandledException;

            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);

            ApplicationConfiguration.Initialize();
            Application.Run(new Form1());
        }

        private static void OnThreadException(object sender, ThreadExceptionEventArgs e)
        {
            HandleFatalError(e.Exception);
        }

        private static void OnUnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            if (e.ExceptionObject is Exception ex)
                HandleFatalError(ex);
            else
                ShowGenericFatalMessage();
        }

        private static void HandleFatalError(Exception ex)
        {
#if DEBUG
            MessageBox.Show(
                ex.ToString(),
                "Kritik Hata (DEBUG)",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
#else
            MessageBox.Show(
                "Beklenmeyen bir hata oluþtu. Uygulama güvenli þekilde kapatýlacak.",
                "Kritik Hata",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
#endif
            Environment.Exit(1);
        }

        private static void ShowGenericFatalMessage()
        {
            MessageBox.Show(
                "Bilinmeyen bir hata oluþtu. Uygulama kapatýlýyor.",
                "Kritik Hata",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);

            Environment.Exit(1);
        }
    }
}
