using FluentAssertions;
using NDepend.Attributes;
using NEdifis;
using NEdifis.Attributes;
using NUnit.Framework;

namespace WpfCalculatorApp.Core.Operations
{
    [TestFixtureFor(typeof(CosineOperation))]
    [UncoverableByTest]
    // ReSharper disable once InconsistentNaming
    internal class CosineOperation_Should
    {

        [Test]
        public void Be_Creatable()
        {
            var ctx = new ContextFor<CosineOperation>();
            var sut = ctx.BuildSut();

            sut.Should().NotBeNull();
        }

        [TestCase(0, 1)]
        [TestCase(3, -0.98999249660044542)]
        [TestCase(-3, -0.98999249660044542)]
        [TestCase(2100, 0.15407274591910985)]
        [TestCase(5555100.00, -0.70990118944234393)]
        [TestCase(double.MinValue, double.MinValue)]
        [TestCase(double.MinValue + 1, double.MinValue)]
        [TestCase(double.MaxValue, double.MaxValue)]
        [TestCase(double.MaxValue + 1, double.MaxValue)]
        public void Do_Cosines(double operand, double expectedResult)
        {
            var ctx = new ContextFor<CosineOperation>();
            var sut = ctx.BuildSut();

            sut.Calculate(operand).Should().Be(expectedResult);
        }

    }
}
