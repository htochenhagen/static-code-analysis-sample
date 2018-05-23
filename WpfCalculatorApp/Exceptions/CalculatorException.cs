﻿using System;
using NDepend.Attributes;

namespace WpfCalculatorApp.Exceptions
{
    [UncoverableByTest]
    public class CalculatorException : Exception
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