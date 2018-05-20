namespace WpfCalculatorApp.Interfaces
{
    public interface IBinaryOperation : IOperation
    {
        double Calculate(double firstOperand, double secondOperand);
    }
}
