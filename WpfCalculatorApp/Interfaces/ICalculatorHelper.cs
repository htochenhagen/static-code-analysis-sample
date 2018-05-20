using System;

namespace WpfCalculatorApp.Interfaces
{
    public interface ICalculatorHelper
    {
        void HandleCalculatorErrors(Action action);
        void SetupCalculator(ICalculator calculator);
        int GetDecimalSeparatorPositionOfString(string value);
        string RemoveNumberGroupSeparator(string value);
        int GetNumberOfDecimalPlaces(string value);
        double ParseDisplayValue(string value);
        string FormatDisplayValue(double value);
    }
}