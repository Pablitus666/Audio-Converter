namespace AudioConverter;

static class Program
{
    /// <summary>
    ///  The main entry point for the application.
    /// </summary>
    [STAThread]
    static void Main()
    {
        try
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            Application.Run(new Form1());
        }
        catch (Exception ex)
        {
            string crashLogPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "crashlog.txt");
            File.WriteAllText(crashLogPath, $"{DateTime.Now}\n\n{ex.ToString()}");
            CustomMessageBoxForm.Show($"A critical error occurred and the application must close. Please check the crashlog.txt file for details.\n\nError: {ex.Message}", "Application Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}