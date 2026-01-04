using System;
using System.IO;
using System.Windows.Forms; // Temporarily added for MessageBox for debugging

namespace AudioConverter.Helpers
{
    public static class Logger
    {
        private static readonly string logFilePath;

        static Logger()
        {
            string appDirectory = AppContext.BaseDirectory; // More reliable for base directory in published apps
            logFilePath = Path.Combine(appDirectory, "AudioConverter_Log.txt");

            // Force log file creation and add a startup message
            try
            {
                File.WriteAllText(logFilePath, $"--- Log Started: {DateTime.Now} ---{Environment.NewLine}");
            }
            catch (Exception ex)
            {
                // Temporary: Show message box if log file cannot be created
                MessageBox.Show($"CRITICAL ERROR: No se pudo crear o escribir en el archivo de log: {logFilePath}{Environment.NewLine}Detalles: {ex.Message}", "Error de Log", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public static void Log(string message, bool isError = false)
        {
            string formattedMessage = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss.fff} [{(isError ? "ERROR" : "INFO")}] {message}";
            
            // Write to Debug output
            System.Diagnostics.Debug.WriteLine(formattedMessage);

            // Write to log file
            try
            {
                File.AppendAllText(logFilePath, formattedMessage + Environment.NewLine);
            }
            catch (Exception ex)
            {
                // Fallback for logging errors if file write fails, and show message box if it's the initial write issue
                System.Diagnostics.Debug.WriteLine($"ERROR: Failed to write to log file: {ex.Message}");
                // If it's a persistent write issue, the initial MessageBox would have fired.
            }
        }
    }
}
