using System;
using FluentAssertions;
using NDepend.Attributes;
using NEdifis.Attributes;
using NUnit.Framework;

namespace WpfCalculatorApp.Properties
{
    [TestFixtureFor(typeof(Config))]
    [UncoverableByTest]
    // ReSharper disable InconsistentNaming
    internal class Config_Should
    {
        [Test]
        public void Use_ConfigService()
        {
            var sut = Activator.CreateInstance<WpfCalculatorApp.Properties.Config>();
            sut.Reset();

            sut.Subsystem.Should().Be("WpfCalculatorApp");
            sut.Application.Should().Be("WpfCalculatorApp");

            sut.ConfigClassesPattern.Should().Be(".*ConfigSettings$");
            sut.GlobalParameterPattern.Should().Be(".*GlobalParameters$");
        }

        [Test]
        public void Configure_Logging()
        {
            var sut = Activator.CreateInstance<WpfCalculatorApp.Properties.Config>();
            sut.Reset();

            sut.LogConfig.Should().Be("NLog.config");
            sut.LogConfigDebug.Should().Be("NLog.Debug.config");
        }
    }
}