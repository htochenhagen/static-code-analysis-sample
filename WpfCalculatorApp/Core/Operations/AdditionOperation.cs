using WpfCalculatorApp.Enums;
using WpfCalculatorApp.Interfaces;

namespace WpfCalculatorApp.Core.Operations
{
    public class AdditionOperation : IBinaryOperation
    {

        public AdditionOperation()
        {
            Operator = CalculatorOperator.Addition;
        }

        public CalculatorOperator Operator { get; }

        public double Calculate(double firstOperand, double secondOperand)
        {
            return firstOperand + secondOperand;
        }
    }
}
