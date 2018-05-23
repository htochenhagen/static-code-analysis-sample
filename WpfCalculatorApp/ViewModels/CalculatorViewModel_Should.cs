using System;
using System.Threading;
using FluentAssertions;
using NDepend.Attributes;
using NEdifis;
using NEdifis.Attributes;
using NUnit.Framework;
using WpfCalculatorApp.Core;
using WpfCalculatorApp.Helpers;

namespace WpfCalculatorApp.ViewModels
{
    //[IsNotDeadCode]
    [UncoverableByTest]
    [TestFixtureFor(typeof(CalculatorViewModel))]
    // ReSharper disable once InconsistentNaming
    internal sealed class CalculatorViewModel_Should
    {

        private CalculatorHelper _calculatorHelper;
        private Calculator _calculator;

        //[IsNotDeadCode]
        [SetUp]
        public void Setup()
        {
            var calculatorContext = new CalculatorContext();
            _calculatorHelper = new CalculatorHelper();
            _calculator = new Calculator(calculatorContext);
            _calculatorHelper.SetupCalculator(_calculator);
        }

        [Apartment(ApartmentState.STA)]
        [STAThread]
        [Test]
        //[IsNotDeadCode]
        public void Be_Creatable()
        {
            var ctx = new ContextFor<CalculatorViewModel>();
            var sut = ctx.BuildSut();

            sut.Should().NotBeNull();
        }

        [Apartment(ApartmentState.STA)]
        [STAThread]
        [Test]
        [TestCase("3", "2", "5")]
        [TestCase("-3", "2,34", "-0,66")]
        [TestCase("2.100", "-300", "1.800")]
        //[IsNotDeadCode]
        public void Do_Press_Plus_Sign(string firstOperand, string secondOperand, string result)
        {
            var sut = new CalculatorViewModel(_calculator, _calculatorHelper);

            sut.CurrentDisplayValue = firstOperand;

            sut.DoPressPlusSign();

            sut.CurrentDisplayValue = secondOperand;

            sut.DoPressEqualSign();

            sut.CurrentDisplayValue.Should().Be(result);
        }

        [Apartment(ApartmentState.STA)]
        [STAThread]
        [Test]
        [TestCase("3", "2", "1")]
        [TestCase("-3", "2,34", "-5,34")]
        [TestCase("2.100", "-300", "2.400")]
        //[IsNotDeadCode]
        public void Do_Press_Minus_Sign(string firstOperand, string secondOperand, string result)
        {
            var sut = new CalculatorViewModel(_calculator, _calculatorHelper);

            sut.CurrentDisplayValue = firstOperand;

            sut.DoPressMinusSign();

            sut.CurrentDisplayValue = secondOperand;

            sut.DoPressEqualSign();

            sut.CurrentDisplayValue.Should().Be(result);
        }

        [Apartment(ApartmentState.STA)]
        [STAThread]
        [Test]
        [TestCase("3", "2", "6")]
        [TestCase("-3", "3", "-9")]
        [TestCase("2.100", "-300", "-630.000")]
        //[IsNotDeadCode]
        public void Do_Press_X_Letter(string firstOperand, string secondOperand, string result)
        {
            var sut = new CalculatorViewModel(_calculator, _calculatorHelper);

            sut.CurrentDisplayValue = firstOperand;

            sut.DoPressXLetter();

            sut.CurrentDisplayValue = secondOperand;

            sut.DoPressEqualSign();

            sut.CurrentDisplayValue.Should().Be(result);
        }

        [Apartment(ApartmentState.STA)]
        [STAThread]
        [Test]
        [TestCase("3", "3", "1")]
        [TestCase("-3", "3", "-1")]
        [TestCase("2.100", "-300", "-7")]
        //[IsNotDeadCode]
        public void Do_Press_Percentage_Sign(string firstOperand, string secondOperand, string result)
        {
            var sut = new CalculatorViewModel(_calculator, _calculatorHelper);

            sut.CurrentDisplayValue = firstOperand;

            sut.DoPressPercentageSign();

            sut.CurrentDisplayValue = secondOperand;

            sut.DoPressEqualSign();

            sut.CurrentDisplayValue.Should().Be(result);
        }

        [Apartment(ApartmentState.STA)]
        [STAThread]
        [Test]
        [TestCase("0", "1")]
        //[IsNotDeadCode]
        public void Do_Press_Cosinus(string firstOperand, string result)
        {
            var sut = new CalculatorViewModel(_calculator, _calculatorHelper);

            sut.CurrentDisplayValue = firstOperand;

            sut.DoPressCosinus();

            sut.CurrentDisplayValue.Should().Be(result);
        }

        [Apartment(ApartmentState.STA)]
        [STAThread]
        [Test]
        [TestCase("0", "0")]
        //[IsNotDeadCode]
        public void Do_Press_Sinus(string firstOperand, string result)
        {
            var sut = new CalculatorViewModel(_calculator, _calculatorHelper);

            sut.CurrentDisplayValue = firstOperand;

            sut.DoPressSinus();

            sut.CurrentDisplayValue.Should().Be(result);
        }

        [Apartment(ApartmentState.STA)]
        [STAThread]
        [Test]
        [TestCase("3", "3", "1")]
        [TestCase("-3", "3", "-1")]
        [TestCase("2.100", "-300", "-7")]
        //[IsNotDeadCode]
        public void Do_Press_Equal_Sign(string firstOperand, string secondOperand, string result)
        {
            var sut = new CalculatorViewModel(_calculator, _calculatorHelper);

            sut.CurrentDisplayValue = firstOperand;

            sut.DoPressPercentageSign();

            sut.CurrentDisplayValue = secondOperand;

            sut.DoPressEqualSign();

            sut.CurrentDisplayValue.Should().Be(result);
        }

        [Apartment(ApartmentState.STA)]
        [STAThread]
        [Test]
        [TestCase("3", "3", "33")]
        [TestCase("4", "3", "43")]
        [TestCase("0", "3", "3")]
        //[IsNotDeadCode]
        public void Do_Press_Number(string firstValue, string secondValue, string result)
        {
            var sut = new CalculatorViewModel(_calculator, _calculatorHelper);

            sut.DoPressNumber(firstValue);
            sut.DoPressNumber(secondValue);
            sut.CurrentDisplayValue.Should().Be(result);
        }

        [Apartment(ApartmentState.STA)]
        [STAThread]
        [Test]
        [TestCase("3", "3", "3,3")]
        [TestCase("4", "3", "4,3")]
        [TestCase("0", "3", "0,3")]
        //[IsNotDeadCode]
        public void Do_Press_Decimal_Separator(string firstValue, string secondValue, string result)
        {
            var sut = new CalculatorViewModel(_calculator, _calculatorHelper);

            sut.DoPressNumber(firstValue);
            sut.DoPressDecimalSeparator();
            sut.DoPressNumber(secondValue);
            sut.CurrentDisplayValue.Should().Be(result);
        }

        [Apartment(ApartmentState.STA)]
        [STAThread]
        [Test]
        [TestCase("3", "-3")]
        [TestCase("-4", "4")]
        [TestCase("0", "-0")]
        //[IsNotDeadCode]
        public void Do_Press_Plus_And_Minus_Sign(string value, string result)
        {
            var sut = new CalculatorViewModel(_calculator, _calculatorHelper);

            sut.CurrentDisplayValue = value;
            sut.DoPressPlusAndMinusSign();
            sut.CurrentDisplayValue.Should().Be(result);
        }

        [Apartment(ApartmentState.STA)]
        [STAThread]
        [TestCase("3")]
        [TestCase("-3")]
        [TestCase("2.100")]
        //[IsNotDeadCode]
        public void Do_Press_Clear(string firstOperand)
        {
            var ctx = new ContextFor<CalculatorViewModel>();
            var sut = ctx.BuildSut();

            sut.CurrentDisplayValue = firstOperand;

            sut.DoPressClear();

            sut.CurrentDisplayValue.Should().Be("0");
        }

    }
}
