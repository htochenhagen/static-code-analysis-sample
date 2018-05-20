using System.Collections.Generic;

namespace WpfCalculatorApp.Interfaces
{
    public interface ICalculator
    {
        bool IsUnaryOperation { get; }
        bool IsBinaryOperation { get; }
        ICalculationContext CalculationContext { get; }
        List<IUnaryOperation> UnaryOperations { get; }
        List<IBinaryOperation> BinaryOperations { get; }
        void AddUnaryOperation(IUnaryOperation operation);
        void AddBinaryOperation(IBinaryOperation operation);
        double Calculate();
        void Reset();
    }
}
