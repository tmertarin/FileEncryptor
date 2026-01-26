using System;
using System.Threading;
using System.Windows.Forms;

namespace FileEncryptor
{
    internal static class Program
    {
        [STAThread]
        private static void Main()
        {
            // UI thread errors
            Application.ThreadException += OnThreadException;

            // Non-UI (Task / background) errors
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
            {
                HandleFatalError(ex);
            }
            else
            {
                ShowGenericFatalMessage();
            }
        }

        private static void HandleFatalError(Exception ex)
        {
#if DEBUG
            MessageBox.Show(
                ex.ToString(),
                "Critical Error (DEBUG)", // "Kritik Hata (DEBUG)" -> Translated
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
#else
            MessageBox.Show(
                "An unexpected error occurred. The application will close safely.", // Translated
                "Critical Error", // Translated
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
#endif
            Environment.Exit(1);
        }

        private static void ShowGenericFatalMessage()
        {
            MessageBox.Show(
                "An unknown error occurred. Application is closing.", // Translated
                "Critical Error", // Translated
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);

            Environment.Exit(1);
        }
    }
}
