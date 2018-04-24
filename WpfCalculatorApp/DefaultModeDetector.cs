using System;
using System.Diagnostics;

namespace WpfCalculatorApp
{
    internal class DefaultModeDetector : IModeDetector
    {
        private readonly string _processName = Process.GetCurrentProcess().ProcessName;

        public bool IsDebug
        {
            get
            {
#if (DEBUG)
                // ReSharper disable once ConvertPropertyToExpressionBody
                return true;
#else // ReSharper disable once ConvertPropertyToExpressionBody
                return false;
#endif
            }
        }

        public bool IsTrace
        {
            get
            {
#if (TRACE)
                // ReSharper disable once ConvertPropertyToExpressionBody
                return true;
#else // ReSharper disable once ConvertPropertyToExpressionBody
                return false;
#endif
            }
        }

        public bool IsRelease => !IsDebug;

        public bool EnableCaliburnLogger
        {
            get
            {
#if (CALIBURNLOGGER) // ReSharper disable once ConvertPropertyToExpressionBody
                return true;
#else
                // ReSharper disable once ConvertPropertyToExpressionBody
                return false;
#endif
            }
        }

        public bool EnableAutofacLogger
        {
            get
            {
#if (AUTOFACLOGGER) // ReSharper disable once ConvertPropertyToExpressionBody
                return true;
#else
                // ReSharper disable once ConvertPropertyToExpressionBody
                return false;
#endif
            }
        }

        public bool EnableEventAggregatorLogger
        {
            get
            {
#if (EVENTLOGGER) // ReSharper disable once ConvertPropertyToExpressionBody
                return true;
#else
                // ReSharper disable once ConvertPropertyToExpressionBody
                return false;
#endif

            }
        }

        public bool IsNcrunch
        {
            get
            {
#if (NCRUNCH) // ReSharper disable once ConvertPropertyToExpressionBody
                return true;
#else
                // ReSharper disable once ConvertPropertyToExpressionBody
                return false;
#endif
            }
        }

        public bool IsResharper => _processName.StartsWith("JetBrains.ReSharper");

        public bool IsNUnit => _processName.StartsWith("nunit");

        public bool UnitTesting => IsNcrunch || IsNUnit || IsResharper;

        public void AttachDebugger(Func<bool> when = null)
        {
            var safeWhen = when ?? (() => !Debugger.IsAttached && IsDebug && !UnitTesting);
            if (safeWhen())
                Debugger.Launch();
        }
    }
}