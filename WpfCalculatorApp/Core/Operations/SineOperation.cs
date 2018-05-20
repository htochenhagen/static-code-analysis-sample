using System;
using WpfCalculatorApp.Enums;
using WpfCalculatorApp.Interfaces;

namespace WpfCalculatorApp.Core.Operations
{
    public class SineOperation : IUnaryOperation
    {
        public SineOperation()
        {
            Operator = CalculatorOperator.Sine;
        }

        public CalculatorOperator Operator { get; }

        public double Calculate(double operand)
        {
            return Math.Sin(operand);
        }
    }
}
