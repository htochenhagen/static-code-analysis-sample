using System;

namespace WpfCalculatorApp.Exceptions
{
    public class CalculatorContextException : ApplicationException
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