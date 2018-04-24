using NEdifis.Attributes;
using NUnit.Framework;

namespace WpfCalculatorApp
{
    [TestFixtureFor(typeof(Program))]
// ReSharper disable InconsistentNaming
    internal class Program_Should
    {
        [Test]
        [Explicit]
        //[Isolated]
        public void Enter_at_Main()
        {
            Program.Main(new string[] { });
        }
    }
}