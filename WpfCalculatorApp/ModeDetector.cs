using System;

namespace WpfCalculatorApp
{
    public class ModeDetector
    {
        public static IModeDetector Current { get; private set; } = new DefaultModeDetector();

        public static bool IsDebug => Current.IsDebug;
        public static bool IsTrace => Current.IsTrace;
        public static bool IsRelease => Current.IsRelease;
        public static bool EnableCaliburnLogger => Current.EnableCaliburnLogger;
        public static bool EnableAutofacLogger => Current.EnableAutofacLogger;
        public static bool EnableEventAggregatorLogger => Current.EnableEventAggregatorLogger;
        public static bool IsNcrunch => Current.IsNcrunch;
        public static bool IsResharper => Current.IsResharper;
        public static bool IsNUnit => Current.IsNUnit;
        public static bool UnitTesting => Current.UnitTesting;

        public static void AttachDebugger(Func<bool> when = null)
        {
            Current.AttachDebugger(when);
        }

        public class TestWith : IDisposable
        {
            private readonly IModeDetector _currentTemp;

            // ReSharper disable ArrangeStaticMemberQualifier
            public TestWith(IModeDetector mock)
            {
                _currentTemp = ModeDetector.Current;
                ModeDetector.Current = mock;
            }

            public void Dispose()
            {
                ModeDetector.Current = _currentTemp;
            }
            // ReSharper restore ArrangeStaticMemberQualifier
        }
    }
}