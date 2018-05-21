using FluentAssertions;
using NEdifis;
using NEdifis.Attributes;
using NUnit.Framework;

namespace WpfCalculatorApp.Core.Operations
{
    [TestFixtureFor(typeof(MultiplicationOperation))]
    // ReSharper disable once InconsistentNaming
    internal class MultiplicationOperation_Should
    {

        [Test]
        public void Be_Creatable()
        {
            var ctx = new ContextFor<MultiplicationOperation>();
            var sut = ctx.BuildSut();

            sut.Should().NotBeNull();
        }

        [TestCase(3, 3, 9)]
        [TestCase(-3, 3, -9)]
        [TestCase(2100, -300, -630000)]
        [TestCase(555555.00, -555.00, -308333025)]
        [TestCase(double.MinValue - 1, 1, double.MinValue - 1)]
        [TestCase(double.MinValue, -10, double.MinValue * -10)]
        [TestCase(double.MaxValue + 1, -1, (double.MaxValue + 1) * -1)]
        [TestCase(double.MaxValue, 10, double.MaxValue * 10)]
        public void Do_Multiplications(double firstOperand, double secondOperand, double expectedResult)
        {
            var ctx = new ContextFor<MultiplicationOperation>();
            var sut = ctx.BuildSut();

            sut.Calculate(firstOperand, secondOperand).Should().Be(expectedResult);
        }

    }
}
