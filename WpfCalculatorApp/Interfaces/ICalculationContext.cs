using WpfCalculatorApp.Enums;

namespace WpfCalculatorApp.Interfaces
{
    public interface ICalculationContext
    {
        CalculatorOperator Operator { get; set; }
        double FirstOperand { get; set; }
        double? SecondOperand { get; set; }
        void Reset();
    }
}
