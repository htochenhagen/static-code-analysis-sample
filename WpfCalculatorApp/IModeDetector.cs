using System;

namespace WpfCalculatorApp
{
    public interface IModeDetector
    {
        bool IsDebug { get; }
        bool IsTrace { get; }
        bool IsRelease { get; }
        bool EnableCaliburnLogger { get; }
        bool EnableAutofacLogger { get; }
        bool EnableEventAggregatorLogger { get; }
        bool IsNcrunch { get; }
        bool IsResharper { get; }
        bool IsNUnit { get; }
        bool UnitTesting { get; }
        void AttachDebugger(Func<bool> when = null);
    }
}