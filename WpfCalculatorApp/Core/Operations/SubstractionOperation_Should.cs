using FluentAssertions;
using NDepend.Attributes;
using NEdifis;
using NEdifis.Attributes;
using NUnit.Framework;

namespace WpfCalculatorApp.Core.Operations
{
    [TestFixtureFor(typeof(SubstractionOperation))]
    [UncoverableByTest]
    //[IsNotDeadCode]
    // ReSharper disable once InconsistentNaming
    internal sealed class SubstractionOperation_Should
    {

        [Test]
        public void Be_Creatable()
        {
            var ctx = new ContextFor<SubstractionOperation>();
            var sut = ctx.BuildSut();

            sut.Should().NotBeNull();
        }

        [TestCase(3, 2, 1)]
        [TestCase(-3, 2.50, -5.5)]
        [TestCase(2100, -300, 2400)]
        [TestCase(5555100.00, -385888800.00, 391443900)]
        [TestCase(double.MinValue - 1, 1, double.MinValue)]
        [TestCase(double.MinValue, -10, double.MinValue - 10)]
        [TestCase(double.MaxValue + 1, -1, double.MaxValue)]
        [TestCase(double.MaxValue, 10, double.MaxValue + 10)]
        public void Do_Substractions(double firstOperand, double secondOperand, double expectedResult)
        {
            var ctx = new ContextFor<SubstractionOperation>();
            var sut = ctx.BuildSut();

            sut.Calculate(firstOperand, secondOperand).Should().Be(expectedResult);
        }

    }
}
