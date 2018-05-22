using FluentAssertions;
using NDepend.Attributes;
using NEdifis;
using NEdifis.Attributes;
using NUnit.Framework;
using WpfCalculatorApp.Core;

namespace WpfCalculatorApp.Helpers
{
    [TestFixtureFor(typeof(CalculatorHelper))]
    [UncoverableByTest]
    // ReSharper disable once InconsistentNaming
    internal class CalculatorHelper_Should
    {
        [Test]
        public void Be_Creatable()
        {
            var ctx = new ContextFor<CalculatorHelper>();
            var sut = ctx.BuildSut();

            sut.Should().NotBeNull();
        }

        [Test]
        public void Setup_Calculator()
        {
            var ctx = new ContextFor<CalculatorHelper>();
            var sut = ctx.BuildSut();

            var calculator = new Calculator(new CalculatorContext());
            sut.SetupCalculator(calculator);

            calculator.UnaryOperations.Should().HaveCount(2);
            calculator.BinaryOperations.Should().HaveCount(4);
        }

        [TestCase("23,322", 2)]
        [TestCase("2", -1)]
        [TestCase("2,322", 1)]
        [TestCase(",322", 0)]
        public void Get_Decimal_Separator_Position_Of_String(string input, int result)
        {
            var ctx = new ContextFor<CalculatorHelper>();
            var sut = ctx.BuildSut();

            sut.GetDecimalSeparatorPositionOfString(input).Should().Be(result);
        }

        [TestCase("23.322", "23322")]
        [TestCase("2", "2")]
        [TestCase("2.322", "2322")]
        [TestCase("", "")]
        public void Remove_Number_Group_Separator(string input, string result)
        {
            var ctx = new ContextFor<CalculatorHelper>();
            var sut = ctx.BuildSut();

            sut.RemoveNumberGroupSeparator(input).Should().Be(result);
        }

        [TestCase("23,322,000", 3)]
        [TestCase("2,1", 1)]
        [TestCase("212", 0)]
        [TestCase(",3222", 0)]
        public void Get_Number_Of_Decimal_Places(string input, int result)
        {
            var ctx = new ContextFor<CalculatorHelper>();
            var sut = ctx.BuildSut();

            sut.GetNumberOfDecimalPlaces(input).Should().Be(result);
        }

        [TestCase("23.322,000", 23322)]
        [TestCase("2,1", 2.1)]
        [TestCase("212", 212)]
        [TestCase(",3222", 0.3222)]
        public void Parse_Display_Value(string input, double result)
        {
            var ctx = new ContextFor<CalculatorHelper>();
            var sut = ctx.BuildSut();

            sut.ParseDisplayValue(input).Should().Be(result);
        }

        [TestCase(23322, "23.322")]
        [TestCase(2.1, "2,1")]
        [TestCase(212, "212")]
        [TestCase(0.3222, "0,3222")]
        public void Format_Display_Value(double input, string result)
        {
            var ctx = new ContextFor<CalculatorHelper>();
            var sut = ctx.BuildSut();

            sut.FormatDisplayValue(input).Should().Be(result);
        }

        [Test]
        public void Handle_Calculator_Errors()
        {
            var ctx = new ContextFor<CalculatorHelper>();
            var sut = ctx.BuildSut();

            sut.HandleCalculatorErrors(() => {});
        }


    }
}
