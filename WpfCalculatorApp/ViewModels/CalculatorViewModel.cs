using System.Threading;
using Caliburn.Micro;
using WpfCalculatorApp.Enums;
using WpfCalculatorApp.Exceptions;
using WpfCalculatorApp.Interfaces;

namespace WpfCalculatorApp.ViewModels
{
    public class CalculatorViewModel : Screen, ICalculatorViewModel
    {

        #region Members

        private bool _resetInput;
        private string _currentDisplayValue;

        #endregion

        #region Class

        public CalculatorViewModel(ICalculator calculator, ICalculatorHelper calculatorHelper)
        {
            Calculator = calculator;
            CalculatorHelper = calculatorHelper;

            Reset();

            calculatorHelper.SetupCalculator(calculator);
        }

        #endregion

        #region Properties

        public string CurrentDisplayValue
        {
            get => _currentDisplayValue;
            set
            {
                _currentDisplayValue = value;
                NotifyOfPropertyChange();
            }
        }

        public ICalculator Calculator { get; }
        public ICalculatorHelper CalculatorHelper { get; }

        #endregion

        #region Methods

        #region "DoPress" Methods

        public void DoPressNumber(string numberAsString)
        {
            CalculatorHelper.HandleCalculatorErrors((() =>
            {
                if (_resetInput)
                {
                    CurrentDisplayValue = "0";
                    _resetInput = false;
                }

                CurrentDisplayValue = CalculatorHelper.RemoveNumberGroupSeparator(CurrentDisplayValue) + numberAsString;

                if (double.TryParse(CurrentDisplayValue, out var number))
                {
                    CurrentDisplayValue = number.ToString($"N{CalculatorHelper.GetNumberOfDecimalPlaces(CurrentDisplayValue)}");
                }
            }));
        }

        public void DoPressDecimalSeparator()
        {
            if (CalculatorHelper.GetDecimalSeparatorPositionOfString(CurrentDisplayValue) < 0)
            {
                CurrentDisplayValue += Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator;
            }
        }

        public void DoPressPlusSign()
        {
            CalculatorHelper.HandleCalculatorErrors((() =>
            {
                Calculator.CalculationContext.FirstOperand = CalculatorHelper.ParseDisplayValue(CurrentDisplayValue);
                Calculator.CalculationContext.Operator = CalculatorOperator.Addition;

                _resetInput = true;
            }));
        }

        public void DoPressMinusSign()
        {
            CalculatorHelper.HandleCalculatorErrors((() =>
            {
                Calculator.CalculationContext.FirstOperand = CalculatorHelper.ParseDisplayValue(CurrentDisplayValue);
                Calculator.CalculationContext.Operator = CalculatorOperator.Substraction;

                _resetInput = true;
            }));
        }

        public void DoPressPlusAndMinusSign()
        {
            CalculatorHelper.HandleCalculatorErrors((() =>
            {
                if (CurrentDisplayValue == null)
                {
                    throw new CalculatorException("No value was entered!");
                }

                CurrentDisplayValue = CurrentDisplayValue.StartsWith("-")
                    ? CurrentDisplayValue.Substring(1)
                    : "-" + CurrentDisplayValue;
            }));
        }

        public void DoPressXLetter()
        {
            CalculatorHelper.HandleCalculatorErrors((() =>
            {
                Calculator.CalculationContext.FirstOperand = CalculatorHelper.ParseDisplayValue(CurrentDisplayValue);
                Calculator.CalculationContext.Operator = CalculatorOperator.Multiplication;

                _resetInput = true;
            }));
        }

        public void DoPressPercentageSign()
        {
            CalculatorHelper.HandleCalculatorErrors((() =>
            {
                Calculator.CalculationContext.FirstOperand = CalculatorHelper.ParseDisplayValue(CurrentDisplayValue);
                Calculator.CalculationContext.Operator = CalculatorOperator.Division;

                _resetInput = true;
            }));
        }

        public void DoPressEqualSign()
        {
            Calculate();
        }

        public void DoPressCosinus()
        {
            CalculatorHelper.HandleCalculatorErrors((() =>
            {
                Calculator.CalculationContext.FirstOperand = CalculatorHelper.ParseDisplayValue(CurrentDisplayValue);
                Calculator.CalculationContext.Operator = CalculatorOperator.Cosine;

                Calculate();
                _resetInput = true;
            }));
        }

        public void DoPressSinus()
        {
            CalculatorHelper.HandleCalculatorErrors((() =>
            {
                Calculator.CalculationContext.FirstOperand = CalculatorHelper.ParseDisplayValue(CurrentDisplayValue);
                Calculator.CalculationContext.Operator = CalculatorOperator.Sine;

                Calculate();
                _resetInput = true;
            }));
        }

        public void DoPressClear()
        {
            Reset();
        } 

        #endregion

        private void Calculate()
        {
            CalculatorHelper.HandleCalculatorErrors((() =>
            {
                if (Calculator.IsBinaryOperation)
                {
                    Calculator.CalculationContext.SecondOperand = CalculatorHelper.ParseDisplayValue(CurrentDisplayValue);
                }
                else if (Calculator.IsUnaryOperation)
                {
                    Calculator.CalculationContext.FirstOperand = CalculatorHelper.ParseDisplayValue(CurrentDisplayValue);
                }

                var result = Calculator.Calculate();

                Reset();

                CurrentDisplayValue = CalculatorHelper.FormatDisplayValue(result);
            }));
        }

        private void Reset()
        {
            CurrentDisplayValue = "0";
            Calculator.Reset();
            _resetInput = true;
        } 

        #endregion

    }
}