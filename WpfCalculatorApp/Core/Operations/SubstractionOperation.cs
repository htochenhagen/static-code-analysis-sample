using WpfCalculatorApp.Enums;
using WpfCalculatorApp.Interfaces;

namespace WpfCalculatorApp.Core.Operations
{
    public class SubstractionOperation : IBinaryOperation
    {

        public SubstractionOperation()
        {
            Operator = CalculatorOperator.Substraction;
        }

        public CalculatorOperator Operator { get; }

        public double Calculate(double firstOperand, double secondOperand)
        {
            return firstOperand - secondOperand;
        }
    }
}
