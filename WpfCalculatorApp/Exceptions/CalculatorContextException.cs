using System;
using NDepend.Attributes;

namespace WpfCalculatorApp.Exceptions
{
    [UncoverableByTest]
    public class CalculatorContextException : Exception
    {
        public CalculatorContextException()
        {
        }

        public CalculatorContextException(string message)
            : base(message)
        {
        }

        public CalculatorContextException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}