using WpfCalculatorApp.Enums;
using WpfCalculatorApp.Exceptions;
using WpfCalculatorApp.Interfaces;

namespace WpfCalculatorApp.Core.Operations
{
    public class DivisionOperation : IBinaryOperation
    {

        public DivisionOperation()
        {
            Operator = CalculatorOperator.Division;
        }

        public CalculatorOperator Operator { get; }

        public double Calculate(double firstOperand, double secondOperand)
        {
            if (secondOperand.Equals(0.0))
            {
                throw new CalculatorException("Division by 0 not possible!");
            }

            return firstOperand / secondOperand;
        }
    }
}
