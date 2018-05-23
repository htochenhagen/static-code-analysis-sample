using NDepend.Attributes;
using NEdifis.Attributes;
using NUnit.Framework;

namespace WpfCalculatorApp
{
    [TestFixtureFor(typeof(Program))]
    [UncoverableByTest]
    //[IsNotDeadCode]
    // ReSharper disable InconsistentNaming
    internal sealed class Program_Should
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