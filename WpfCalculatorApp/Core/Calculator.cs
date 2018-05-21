using System.Collections.Generic;
using System.Linq;
using WpfCalculatorApp.Enums;
using WpfCalculatorApp.Exceptions;
using WpfCalculatorApp.Interfaces;

namespace WpfCalculatorApp.Core
{
    public class Calculator : ICalculator
    {

        public Calculator(ICalculationContext calculationContext)
        {
            CalculationContext = calculationContext;
        }

        public List<IOperation> Operations => UnaryOperations.Select(x => x as IOperation).Concat(BinaryOperations.Select(x => x as IOperation)).ToList();

        public ICalculationContext CalculationContext { get; }
        public List<IUnaryOperation> UnaryOperations { get; } = new List<IUnaryOperation>();
        public List<IBinaryOperation> BinaryOperations { get; } = new List<IBinaryOperation>();

        public bool IsUnaryOperation
        {
            get
            {
                if (CalculationContext?.Operator != null)
                {
                    return GetOperation() is IUnaryOperation;
                }

                return false;
            }
        }

        public bool IsBinaryOperation
        {
            get
            {
                if (CalculationContext?.Operator != null)
                {
                    return GetOperation() is IBinaryOperation;
                }

                return false;
            }
        }

        public void AddUnaryOperation(IUnaryOperation operation)
        {
            if (!UnaryOperations.Contains(operation))
            {
                UnaryOperations.Add(operation);
            }
        }

        public void AddBinaryOperation(IBinaryOperation operation)
        {
            if (!BinaryOperations.Contains(operation))
            {
                BinaryOperations.Add(operation);
            }
        }

        public double Calculate()
        {
            Validate();

            var result = 0.0;

            var operation = GetOperation();
            if (operation is IBinaryOperation binaryOperation)
            {
                if (CalculationContext.SecondOperand != null)
                    result = binaryOperation.Calculate(CalculationContext.FirstOperand,
                        CalculationContext.SecondOperand.Value);
            }
            else if(operation is IUnaryOperation unaryOperation)
            {
                result = unaryOperation.Calculate(CalculationContext.FirstOperand);
            }

            return result;
        }

        public void Reset()
        {
            CalculationContext.Reset();
        }

        private void Validate()
        {
            if (CalculationContext == null) throw new CalculatorException("Nothing to calculate!");

            var operation = GetOperation();
            if (operation == null) throw new CalculatorException("No appropriate operation was found!");

            if (operation.Operator == CalculatorOperator.Empty) throw new CalculatorException("No operation was chosen!");
            if (operation is IBinaryOperation && CalculationContext.SecondOperand == null)
                throw new CalculatorException(
                    "Invalid calculator context!\r\n\r\nSecond operand must not be <null> if operation type was set to binary!");
        }

        private IOperation GetOperation()
        {
            var operation = Operations.FirstOrDefault(o => o.Operator == CalculationContext.Operator);
            return operation;
        }

    }
}
