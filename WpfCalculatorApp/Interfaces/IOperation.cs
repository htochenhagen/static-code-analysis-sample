using WpfCalculatorApp.Enums;

namespace WpfCalculatorApp.Interfaces
{
    public interface IOperation
    {
        CalculatorOperator Operator { get; }
    }
}
