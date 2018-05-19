using System;

namespace WpfCalculatorApp
{
    public static class Program
    {
        [STAThread]
        public static void Main(string[] args)
        {
            try
            {
                // now we can finally start the app
                var application = new App();
                application.Run();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            Environment.Exit(0);
        }
    }
}
