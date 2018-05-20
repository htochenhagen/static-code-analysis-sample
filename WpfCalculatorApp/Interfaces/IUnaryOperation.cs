namespace WpfCalculatorApp.Interfaces
{
    public interface IUnaryOperation : IOperation
    {
        double Calculate(double operand);
    }
}
