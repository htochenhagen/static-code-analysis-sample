using WpfCalculatorApp.Enums;
using WpfCalculatorApp.Exceptions;
using WpfCalculatorApp.Interfaces;

namespace WpfCalculatorApp.Core
{
    public class CalculatorContext : ICalculationContext
    {
        private CalculatorOperator _operator;
        private bool _isResetting;

        public CalculatorContext()
        {
            Reset();
        }

        public CalculatorOperator Operator
        {
            get => _operator;
            set
            {
                if (value == CalculatorOperator.Empty && !_isResetting) throw new CalculatorContextException("Invalid calculator context!\r\n\r\nCalculator operator <Empty> is not supported!");
                if (_operator != CalculatorOperator.Empty && !_isResetting) throw new CalculatorException("Only one operation is allowed!");

                _operator = value;
            }
        }

        public double FirstOperand { get; set; }
        public double? SecondOperand { get; set; }

        public void Reset()
        {
            try
            {
                _isResetting = true;

                Operator = CalculatorOperator.Empty;
                FirstOperand = default(double);
                SecondOperand = null;
            }
            finally
            {
                _isResetting = false;
            }
        }

    }
}
