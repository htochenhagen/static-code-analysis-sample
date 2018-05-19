using System;
using System.Threading;
using FluentAssertions;
using NEdifis;
using NEdifis.Attributes;
using NUnit.Framework;

namespace WpfCalculatorApp.ViewModels
{
    [TestFixtureFor(typeof(CalculatorViewModel))]
    // ReSharper disable once InconsistentNaming
    internal class CalculatorViewModel_Should
    {
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
        public void Clear_all_props()
        {
            var ctx = new ContextFor<CalculatorViewModel>();
            var sut = ctx.BuildSut();

            sut.CurrentDisplayValue = "3";
            sut.LastDisplayValue = 2;
            sut.Operator = CalculatorViewModel.CalculatorOperator.Addition;
            sut.Type = CalculatorViewModel.OperationType.Binary;

            sut.DoClear();

            sut.CurrentDisplayValue.Should().Be("0");
            sut.LastDisplayValue.Should().Be(null);
            sut.Operator.Should().Be(CalculatorViewModel.CalculatorOperator.Empty);
            sut.Type.Should().Be(CalculatorViewModel.OperationType.Empty);
        }

    }
}
