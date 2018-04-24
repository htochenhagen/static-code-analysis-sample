using System;
using System.ComponentModel;
using System.Globalization;
using System.Threading;
using System.Windows;
using Caliburn.Micro;

namespace WpfCalculatorApp.ViewModels
{
    public class CalculatorViewModel : Screen, ICalculatorViewModel
    {

        private bool _resetInput;
        private string _currentDisplayValue;
        private double? _lastDisplayValue;

        public enum CalculatorOperator
        {
            Empty,
            Addition,
            Substraction,
            Multiplication,
            Division,
            Cosine,
            Sine
        }
        public enum OperationType
        {
            Empty,
            Unary,
            Binary
        }

        public CalculatorViewModel()
        {
            Reset();
        }

        public CalculatorOperator Operator { get; set; }
        public OperationType Type { get; set; }

        public string CurrentDisplayValue
        {
            get { return _currentDisplayValue; }
            set
            {
                _currentDisplayValue = value;
                NotifyOfPropertyChange();
            }
        }

        public double? LastDisplayValue
        {
            get { return _lastDisplayValue; }
            set
            {
                _lastDisplayValue = value;
                NotifyOfPropertyChange();
            }
        }

        public void DoPressNumber(string numberAsString)
        {
            try
            {
                HandleNumberInput(numberAsString);
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

        public void DoPressDecimalSeparator()
        {
            HandleDecimalSeparatorInput();
        }

        public void DoPressPlusSign()
        {
            try
            {
                HandleAddition();
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

        public void DoPressMinusSign()
        {
            try
            {
                HandleSubstraction();
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

        public void DoPressPlusAndMinusSign()
        {
            try
            {
                HandleNegation();
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

        public void DoPressXLetter()
        {
            try
            {
                HandleMultiplication();
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

        public void DoPressPercentageSign()
        {
            try
            {
                HandleDivision();
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

        public void DoPressEqualSign()
        {
            try
            {
                CurrentDisplayValue = HandleCalculation();
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

        public void DoPressCosinus()
        {
            try
            {
                HandleCosine();
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

        public void DoPressSinus()
        {
            try
            {
                HandleSine();
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

        private double CalculateUnaryOperation(CalculatorOperator calculatorOperator, double operand)
        {
           return Calculate(calculatorOperator, OperationType.Unary, operand);
        }

        private double CalculateBinaryOperation(CalculatorOperator calculatorOperator, double firstOperand, double secondOperand)
        {
            return Calculate(calculatorOperator, OperationType.Binary, firstOperand, secondOperand);
        }

        private double Calculate(CalculatorOperator calculatorOperator, OperationType type, double firstOperand, double? secondOperand = null)
        {
            var result = 0.0;

            if (!Enum.IsDefined(typeof(OperationType), type))
                throw new InvalidEnumArgumentException(nameof(type), (int)type, typeof(OperationType));

            if (type == OperationType.Binary && !secondOperand.HasValue)
            {
                throw new CalculatorException("Second operand is needed!");
            }

            switch (calculatorOperator)
            {
                case CalculatorOperator.Empty:
                    throw new CalculatorException("No operation was chosen!");
                case CalculatorOperator.Addition:
                    if (secondOperand != null) result = firstOperand + secondOperand.Value;
                    break;
                case CalculatorOperator.Substraction:
                    if (secondOperand != null) result = firstOperand - secondOperand.Value;
                    break;
                case CalculatorOperator.Multiplication:
                    if (secondOperand != null) result = firstOperand * secondOperand.Value;
                    break;
                case CalculatorOperator.Division:
                    if (secondOperand.Equals(0.0))
                    {
                        throw new CalculatorException("Division by 0 not possible!");
                    }
                    if (secondOperand != null) result = firstOperand / secondOperand.Value;
                    break;
                case CalculatorOperator.Cosine:
                    result = Math.Cos(firstOperand);
                    break;
                case CalculatorOperator.Sine:
                    result = Math.Sin(firstOperand);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(calculatorOperator), calculatorOperator, null);
            }

            return result;
        }

        private string HandleCalculation()
        {
            if (Operator == CalculatorOperator.Empty)
            {
                throw new CalculatorException("No operation was chosen!");
            }
            if (Type == OperationType.Empty)
            {
                throw new CalculatorException("No operation type was chosen!");
            }

            if (!double.TryParse(CurrentDisplayValue, out var number))
            {
                throw new CalculatorException("Display value could't be parsed!");
            }

            var result = 0.0;
            if (Type == OperationType.Unary)
            {
                result = CalculateUnaryOperation(Operator, number);
            }
            else if (Type == OperationType.Binary)
            {
                if (LastDisplayValue == null)
                {
                    throw new CalculatorException("First operand is needed!");
                }

                result = CalculateBinaryOperation(Operator, LastDisplayValue.Value, number);
            }

            var numberOfDecimalPlaces = 0;
            var resultAsString = result.ToString(CultureInfo.CurrentCulture);
            var decimalSeparatorPosition = GetDecimalSeparatorPositionOfString(resultAsString);
            if (decimalSeparatorPosition > 0)
            {
                numberOfDecimalPlaces = resultAsString.Substring(decimalSeparatorPosition + 1).Length;
            }

            Reset();

            return result.ToString($"N{numberOfDecimalPlaces}");
        }

        private void HandleNumberInput(string numberAsString)
        {
            if (_resetInput)
            {
                CurrentDisplayValue = "0";
                _resetInput = false;
            }

            CurrentDisplayValue =
                CurrentDisplayValue?.Replace(Thread.CurrentThread.CurrentCulture.NumberFormat.NumberGroupSeparator,
                    string.Empty) + numberAsString;

            var numberOfDecimalPlaces = 0;
            var decimalSeparatorPosition = GetDecimalSeparatorPositionOfString(CurrentDisplayValue);
            if (decimalSeparatorPosition > 0)
            {
                numberOfDecimalPlaces = CurrentDisplayValue.Substring(decimalSeparatorPosition + 1).Length;
            }

            if (double.TryParse(CurrentDisplayValue, out var number))
            {
                CurrentDisplayValue = number.ToString($"N{numberOfDecimalPlaces}");
            }
        }
        private void HandleDecimalSeparatorInput()
        {
            if (GetDecimalSeparatorPositionOfString(CurrentDisplayValue) < 0)
            {
                CurrentDisplayValue += Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator;
            }
        }

        private void HandleAddition()
        {
            if (Operator != CalculatorOperator.Empty)
            {
                throw new CalculatorException("Only one operation is allowed!");
            }

            if (double.TryParse(CurrentDisplayValue, out var number))
            {
                LastDisplayValue = number;
            }
            else
            {
                throw new CalculatorException("Display value could't be parsed!");
            }

            Operator = CalculatorOperator.Addition;
            Type = OperationType.Binary;
            _resetInput = true;
        }


        private void HandleSubstraction()
        {
            if (Operator != CalculatorOperator.Empty)
            {
                throw new CalculatorException("Only one operation is allowed!");
            }

            if (double.TryParse(CurrentDisplayValue, out var number))
            {
                LastDisplayValue = number;
            }
            else
            {
                throw new CalculatorException("Display value could't be parsed!");
            }

            Operator = CalculatorOperator.Substraction;
            Type = OperationType.Binary;
            _resetInput = true;
        }


        private void HandleMultiplication()
        {
            if (Operator != CalculatorOperator.Empty)
            {
                throw new CalculatorException("Only one operation is allowed!");
            }

            if (double.TryParse(CurrentDisplayValue, out var number))
            {
                LastDisplayValue = number;
            }
            else
            {
                throw new CalculatorException("Display value could't be parsed!");
            }


            Operator = CalculatorOperator.Multiplication;
            Type = OperationType.Binary;
            _resetInput = true;
        }

        private void HandleDivision()
        {
            if (Operator != CalculatorOperator.Empty)
            {
                throw new CalculatorException("Only one operation is allowed!");
            }

            if (double.TryParse(CurrentDisplayValue, out var number))
            {
                LastDisplayValue = number;
            }
            else
            {
                throw new CalculatorException("Display value couldn't be parsed!");
            }

            Operator = CalculatorOperator.Division;
            Type = OperationType.Binary;
            _resetInput = true;
        }

        private void HandleNegation()
        {
            if (CurrentDisplayValue == null)
            {
                throw new CalculatorException("No value was entered!");
            }

            CurrentDisplayValue = CurrentDisplayValue.StartsWith("-")
                ? CurrentDisplayValue.Substring(1)
                : "-" + CurrentDisplayValue;
        }

        private void HandleCosine()
        {
            if (Operator != CalculatorOperator.Empty)
            {
                throw new CalculatorException("Only one operation is allowed!");
            }

            Operator = CalculatorOperator.Cosine;
            Type = OperationType.Unary;

            CurrentDisplayValue = HandleCalculation();
            _resetInput = true;
        }

        private void HandleSine()
        {
            if (Operator != CalculatorOperator.Empty)
            {
                throw new CalculatorException("Only one operation is allowed!");
            }

            Operator = CalculatorOperator.Sine;
            Type = OperationType.Unary;

            CurrentDisplayValue = HandleCalculation();
            _resetInput = true;
        }


        private static int GetDecimalSeparatorPositionOfString(string value)
        {
            var decimalSeparatorPosition = value.LastIndexOf(",", StringComparison.Ordinal);
            return decimalSeparatorPosition;
        }


        public void DoClear()
        {
            Reset();
        }

        private void Reset()
        {
            CurrentDisplayValue = "0";
            LastDisplayValue = null;
            Operator = CalculatorOperator.Empty;
            Type = OperationType.Empty;
            _resetInput = true;
        }

    }

    class CalculatorException : ApplicationException
    {
        public CalculatorException()
        {
        }

        public CalculatorException(string message)
            : base(message)
        {
        }

        public CalculatorException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }

}