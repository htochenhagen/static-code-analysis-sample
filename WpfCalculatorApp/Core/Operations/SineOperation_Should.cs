using FluentAssertions;
using NDepend.Attributes;
using NEdifis;
using NEdifis.Attributes;
using NUnit.Framework;

namespace WpfCalculatorApp.Core.Operations
{
    [TestFixtureFor(typeof(SineOperation))]
    [UncoverableByTest]
    //[IsNotDeadCode]
    // ReSharper disable once InconsistentNaming
    internal sealed class SineOperation_Should
    {

        [Test]
        public void Be_Creatable()
        {
            var ctx = new ContextFor<SineOperation>();
            var sut = ctx.BuildSut();

            sut.Should().NotBeNull();
        }

        [TestCase(0, 0)]
        [TestCase(3, 0.14112000805986721)]
        [TestCase(-3, -0.14112000805986721)]
        [TestCase(2100, 0.9880595067934651)]
        [TestCase(5555100.00, -0.70430128583465279)]
        [TestCase(double.MinValue, double.MinValue)]
        [TestCase(double.MinValue + 1, double.MinValue)]
        [TestCase(double.MaxValue, double.MaxValue)]
        [TestCase(double.MaxValue + 1, double.MaxValue)]
        public void Do_Sines(double operand, double expectedResult)
        {
            var ctx = new ContextFor<SineOperation>();
            var sut = ctx.BuildSut();

            sut.Calculate(operand).Should().Be(expectedResult);
        }

    }
}
