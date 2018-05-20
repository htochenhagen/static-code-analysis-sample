using System;
using System.Threading;
using FluentAssertions;
using NEdifis;
using NEdifis.Attributes;
using NUnit.Framework;
using WpfCalculatorApp.Core;
using WpfCalculatorApp.Helpers;

namespace WpfCalculatorApp.ViewModels
{
    [TestFixtureFor(typeof(CalculatorViewModel))]
    // ReSharper disable once InconsistentNaming
    internal class CalculatorViewModel_Should
    {

        private CalculatorHelper _calculatorHelper;
        private Calculator _calculator;

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
        [TestCase("3")]
        [TestCase("-3")]
        [TestCase("2.100")]
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
