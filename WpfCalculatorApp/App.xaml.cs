using System;
using System.Globalization;
using System.Threading;
using System.Windows;
using WpfCalculatorApp.Bootstrapping;

namespace WpfCalculatorApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();

            // using the bootstrapper here instead of inside XAML enables better exception handling
            var bootstrapper = new Bootstrapper();

            // don't let the compiler optimize this field away
            // ReSharper disable once ConditionIsAlwaysTrueOrFalse
            // ReSharper disable HeuristicUnreachableCode
            if (bootstrapper == null)
            {
                throw new InvalidOperationException();
            }
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            var de = new CultureInfo("de-DE");
            Thread.CurrentThread.CurrentCulture = de;
            Thread.CurrentThread.CurrentUICulture = de;

            Current.DispatcherUnhandledException += (s, args) =>
            {
                args.Handled = true;
            };

            base.OnStartup(e);

            MainWindow.BringToFront();
        }
    }
}
