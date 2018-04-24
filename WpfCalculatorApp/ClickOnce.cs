using System;
using System.Deployment.Application;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using Microsoft.Win32;

namespace WpfCalculatorApp
{
    internal class ClickOnce
    {
        private readonly Uri _activationUri;

        public string Query => _activationUri?.Query ?? string.Empty;

        public string[] Args => AppDomain.CurrentDomain?.SetupInformation?.ActivationArguments?.ActivationData;

        public ClickOnce()
        {
            try
            {
                _activationUri = (ApplicationDeployment.IsNetworkDeployed &&
                                  ApplicationDeployment.CurrentDeployment.ActivationUri != null)
                       ? ApplicationDeployment.CurrentDeployment.ActivationUri
                       : null;

            }
            catch (Exception ex)
            {
                Trace.TraceWarning("Cannot access clickonce environment: {0}", ex.Message);
                _activationUri = null;
            }
        }

        static ClickOnce()
        {
            //only run if clickonce deployed, on first run only
            if (ApplicationDeployment.IsNetworkDeployed &&
                ApplicationDeployment.CurrentDeployment.IsFirstRun)
                SetAddRemoveProgramsIcon();
        }

        private static void SetAddRemoveProgramsIcon(string iconPath = FrequencyManagerIcon)
        {
            // cf.: http://stackoverflow.com/questions/10927109/custom-icon-for-clickonce-application-in-add-or-remove-programs
            try
            {
                var assembly = Assembly.GetExecutingAssembly();
                var displayName = assembly.GetProduct(); // cf. ClickOnce.targets --> ClickOnce DisplayName

                //the icon is included in this program
                var iconSourcePath = GetIconPath(assembly, iconPath);

                if (!File.Exists(iconSourcePath))
                    return;

                using (var uninstallKey =
                    Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Uninstall"))
                {
                    if (uninstallKey == null)
                        return;

                    var subKeyNames = uninstallKey.GetSubKeyNames();
                    foreach (var subKeyName in subKeyNames)
                    {
                        using (var myKey = uninstallKey.OpenSubKey(subKeyName, true))
                        {
                            var myValue = myKey?.GetValue("DisplayName");
                            if (myValue == null || myValue.ToString() != displayName)
                                continue;

                            myKey.SetValue("DisplayIcon", iconSourcePath);
                            break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Trace.TraceWarning("Could not set Add/Remove application icon: {0}", ex);
            }
        }

        private const string FrequencyManagerIcon = @"Resources\FrequencyManagerIco.ico";

        internal static string GetIconPath(Assembly assembly = null, string iconPath = FrequencyManagerIcon)
        {
            var safeAssembly = assembly ?? Assembly.GetExecutingAssembly();
            var appPath = Path.GetDirectoryName(safeAssembly.GetLocation());
            // ReSharper disable once AssignNullToNotNullAttribute
            return Path.Combine(appPath, iconPath);
        }
    }
}
