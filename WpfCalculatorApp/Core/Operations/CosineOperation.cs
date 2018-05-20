using System;
using WpfCalculatorApp.Enums;
using WpfCalculatorApp.Interfaces;

namespace WpfCalculatorApp.Core.Operations
{
    public class CosineOperation : IUnaryOperation
    {
        public CosineOperation()
        {
            Operator = CalculatorOperator.Cosine;
        }

        public CalculatorOperator Operator { get; }

        public double Calculate(double operand)
        {
            return Math.Cos(operand);
        }
    }
}
