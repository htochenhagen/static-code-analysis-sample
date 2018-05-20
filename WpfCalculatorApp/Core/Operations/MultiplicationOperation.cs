using WpfCalculatorApp.Enums;
using WpfCalculatorApp.Interfaces;

namespace WpfCalculatorApp.Core.Operations
{
    public class MultiplicationOperation : IBinaryOperation
    {

        public MultiplicationOperation()
        {
            Operator = CalculatorOperator.Multiplication;
        }

        public CalculatorOperator Operator { get; }

        public double Calculate(double firstOperand, double secondOperand)
        {
            return firstOperand * secondOperand;
        }
    }
}
