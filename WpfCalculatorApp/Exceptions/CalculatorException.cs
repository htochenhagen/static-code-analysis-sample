using System;

namespace WpfCalculatorApp.Exceptions
{
    public class CalculatorException : ApplicationException
    {
        public CalculatorException()
        {
        }

        public CalculatorException(string message)
            : base(message)
        {
        }

        public CalculatorException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}