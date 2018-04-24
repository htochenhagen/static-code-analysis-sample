using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using Caliburn.Micro;
using NLog;
using NLog.Config;
using NLog.Targets;
using WpfCalculatorApp.Properties;
using LogManager = NLog.LogManager;

namespace WpfCalculatorApp
{
    public static class Program
    {
        private static readonly AppInfo AppInfo = AppInfo.Default;

        [STAThread]
        public static void Main(string[] args)
        {
            ModeDetector.AttachDebugger();

            InitializeLogging(Config.Default);

            var allArgs = AddClickOnceArgs(args);

            try
            {
                var appInfo = string.Format(
                    CultureInfo.InvariantCulture,
                    "{0} - {1}, Version: {2} built {3}, {4} ",
                    AppInfo.Product,
                    AppInfo.Title,
                    AppInfo.Version,
                    AppInfo.BuildDate,
                    AppInfo.Copyright);
                Trace.TraceInformation(appInfo);
                Trace.TraceInformation(AppInfo.SystemInfo);
                Trace.TraceInformation(AppInfo.Assembly.Location);

                // now we can finally start the app
                var application = new App();
                application.Run();

                SaveSettings();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            Environment.Exit(0);
        }

        private static void SaveSettings()
        {
            var settingsPersister = IoC.Get<IEnumerable<ISaveSettings>>().ToList();
            if (!settingsPersister.Any()) return;
            settingsPersister.ForEach(iss => iss.SaveSettings());
        }

        #region logging helper

        private static void InitializeLogging(Config configuration)
        {
            var logconfig = ModeDetector.IsDebug ? configuration.LogConfigDebug : configuration.LogConfig;
            var path = Environment.ExpandEnvironmentVariables(logconfig);
            var logConfigFile = new FileInfo(path);
            if (logConfigFile.Exists)
                LogManager.Configuration = new XmlLoggingConfiguration(logConfigFile.FullName);
            else
            {
                LogManager.Configuration = DefaultLogging(ModeDetector.IsDebug ? LogLevel.Debug : LogLevel.Info);
                // NOTE that we log the warning AFTER initializing logging!
                Trace.TraceWarning("Unable to find log configuration file: '{0}'. Using default configuration", logconfig);
            }

            AddTraceListener();
        }

        private static void AddTraceListener()
        {
            var traceListener = new NLogTraceListener();

            AdditonalTraceSources()
                .ForEach(t => t.Listeners.Add(traceListener));

            Trace.Listeners.Add(traceListener);
        }

        private static LoggingConfiguration DefaultLogging(LogLevel logLevel = null)
        {
            // cf.: http://stackoverflow.com/questions/24070349/nlog-switching-from-nlog-config-to-programmatic-configuration
            var config = new LoggingConfiguration();

            var fileTarget = new FileTarget
            {
                Layout = "${longdate} [${threadid}] ${uppercase:${level}} - ${message}",
                FileName = "${environment:LOCALAPPDATA}/Schönhofer/FrequencyManager/Logs/FrequencyManager_${environment:USERNAME}.${environment:USERDOMAIN}.log",
                Header = "[Open Log]",
                Footer = "[Close Log]",
                ArchiveFileName = "${environment:LOCALAPPDATA}/Schönhofer/FrequencyManager/Logs/FrequencyManager_${environment:USERNAME}.${environment:USERDOMAIN}.{#}.log",
                ArchiveAboveSize = 1048576,
                ArchiveEvery = FileArchivePeriod.None,
                ArchiveNumbering = ArchiveNumberingMode.Rolling,
                MaxArchiveFiles = 5,
                ConcurrentWrites = false,
                KeepFileOpen = true,
                Encoding = Encoding.UTF8
            };

            config.AddTarget("f", fileTarget);

            var rule = new LoggingRule("*", logLevel ?? LogLevel.Debug, fileTarget);
            config.LoggingRules.Add(rule);

            return config;
        }

        private static List<TraceSource> AdditonalTraceSources()
        {
            // Create additional trace sources list
            var additonalTraceSources = new List<TraceSource>();

            if (ModeDetector.IsDebug)
                additonalTraceSources.AddRange(new[]
                {
                    // WPF
                    PresentationTraceSources.DataBindingSource,
                    PresentationTraceSources.DependencyPropertySource,
                    PresentationTraceSources.MarkupSource,
                    PresentationTraceSources.ResourceDictionarySource
                });

            // Add additional trace sources - .NET framework
            //TraceSources.Add(System.Diagnostics.

            return additonalTraceSources;
        }

        #endregion

        private static IList<string> AddClickOnceArgs(IList<string> args)
        {
            // add clickonce uri
            var clickOnceArgs = new ClickOnce().Args;
            if (clickOnceArgs != null && clickOnceArgs.Any())
            {
                var argString = string.Join(" ", clickOnceArgs);
                var urlArgs = string.Format(CultureInfo.InvariantCulture, $"Url=\"{argString}\"");
                return new List<string>(args) { urlArgs };
            }


            var query = new ClickOnce().Query;
            if (string.IsNullOrWhiteSpace(query)) return args;

            var urlArg = string.Format(CultureInfo.InvariantCulture, "Url=\"FrequencyManager://?{0}\"", query).Replace("??", "?"); // sometimes there is a ??

            var addedArgs = new List<string>(args) { urlArg };
            return addedArgs;
        }
    }
}
