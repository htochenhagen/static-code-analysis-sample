using System;

namespace WpfCalculatorApp.ViewModels
{
    class CalculatorException : ApplicationException
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