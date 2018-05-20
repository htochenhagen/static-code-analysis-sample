using System;
using System.Globalization;
using System.Threading;
using System.Windows;
using WpfCalculatorApp.Core.Operations;
using WpfCalculatorApp.Exceptions;
using WpfCalculatorApp.Extensions;
using WpfCalculatorApp.Interfaces;

namespace WpfCalculatorApp.Helpers
{
    public class CalculatorHelper : ICalculatorHelper
    {
        public void HandleCalculatorErrors(Action action)
        {
            try
            {
                action();
            }
            catch (CalculatorContextException e)
            {
                MessageBox.Show(e.Message, "Warnung", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            catch (CalculatorException e)
            {
                MessageBox.Show(e.Message, "Warnung", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.GetErrorMessage(), "Fehler", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public void SetupCalculator(ICalculator calculator)
        {
            calculator.AddUnaryOperation(new CosineOperation());
            calculator.AddUnaryOperation(new SineOperation());
            calculator.AddBinaryOperation(new AdditionOperation());
            calculator.AddBinaryOperation(new DivisionOperation());
            calculator.AddBinaryOperation(new MultiplicationOperation());
            calculator.AddBinaryOperation(new SubstractionOperation());
        }

        public int GetDecimalSeparatorPositionOfString(string value)
        {
            var decimalSeparatorPosition = value.LastIndexOf(",", StringComparison.Ordinal);
            return decimalSeparatorPosition;
        }

        public string RemoveNumberGroupSeparator(string value)
        {
            return value?.Replace(Thread.CurrentThread.CurrentCulture.NumberFormat.NumberGroupSeparator, string.Empty);
        }

        public int GetNumberOfDecimalPlaces(string value)
        {
            var numberOfDecimalPlaces = 0;
            var decimalSeparatorPosition = GetDecimalSeparatorPositionOfString(value);
            if (decimalSeparatorPosition > 0)
            {
                numberOfDecimalPlaces = value.Substring(decimalSeparatorPosition + 1).Length;
            }

            return numberOfDecimalPlaces;
        }

        public double ParseDisplayValue(string value)
        {
            double result;

            if (double.TryParse(value, out var number))
            {
                result = number;
            }
            else
            {
                throw new CalculatorException("Display value could't be parsed!");
            }

            return result;
        }

        public string FormatDisplayValue(double value)
        {
            var numberOfDecimalPlaces = GetNumberOfDecimalPlaces(value.ToString(CultureInfo.CurrentCulture));

            var result = value.ToString($"N{numberOfDecimalPlaces}");

            return result;
        }

    }
}
