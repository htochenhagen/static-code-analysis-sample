using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using FluentAssertions;
using NEdifis.Attributes;
using NUnit.Framework;

namespace WpfCalculatorApp
{
    [TestFixtureFor(typeof (ClickOnce))]
    // ReSharper disable InconsistentNaming
    internal class ClickOnce_Should
    {
        private const string AppRef = @"%USERPROFILE%\Desktop\FrequencyManager.appref-ms";
        private const string FrequencyManagerOpenContent = @"FrequencyManager://?command=report&template=soi&attachments=";

        /// <summary>
        /// Work_s the with_apprefms_ without_ parameter.
        /// </summary>
        [Test]
        [Explicit("automatic testing should NOT start any external apps!")]
        public void Work_With_apprefms_Without_Parameter()
        {
            // use process start to start FrequencyManager
            var p = Process.Start(Environment.ExpandEnvironmentVariables(AppRef), string.Empty);

            // ReSharper disable PossibleNullReferenceException
            p.Should().NotBeNull();
            p.WaitForExit();
            p.ExitCode.Should().Be(0);
            // ReSharper restore PossibleNullReferenceException
        }

        [Test]
        [Explicit("automatic testing should NOT start any external apps!")]
        public void Work_With_apprefms_To_Open_SOI()
        {
            // use process start to start FrequencyManager
            var p = Process.Start(Environment.ExpandEnvironmentVariables(AppRef), FrequencyManagerOpenContent);

            // ReSharper disable PossibleNullReferenceException
            p.Should().NotBeNull();
            p.WaitForExit();
            p.ExitCode.Should().Be(0);
            // ReSharper restore PossibleNullReferenceException
        }

        //[Test]
        //public void Deploy_FrequencyManager_ico_so_it_can_be_set_in_Programs_Add_Remove()
        //{
        //    var iconSourcePath = ClickOnce.GetIconPath();
        //    File.Exists(iconSourcePath).Should().BeTrue(because: "Icon should be deployed with Copy Always");
        //}

        [Ignore("fck test does not work")]
        [Test, Description("#11600: no umlauts in Start Menu")]
        public void Not_contain_umlauts_in_PublisherName()
        {
            Console.WriteLine(Assembly.GetExecutingAssembly().FullName);
            var fileName = Locate("FrequencyManager.Client.targets");
            var clickOnceTargets = File.ReadAllLines(fileName);
            var publisherName = clickOnceTargets.Single(line => line.Contains("<PublisherName>"));

            char[] umlauts = { 'ö', 'ä', 'ü', 'Ö', 'Ä', 'Ü' };
            if (publisherName.IndexOfAny(umlauts) >= 0) Assert.Fail("PublisherName contains umlauts");
        }

        // cf.: http://gitserver.sse.schoenhofer.de/gitblit/blob/?f=MiniMod.TestData/TestDataExtensions.cs&r=common/MiniMods.git&h=master
        private static string Locate(string fileProjectRelativePath)
        {
            var filePath = fileProjectRelativePath.Replace("/", @"\");

            var dir = new DirectoryInfo(Environment.CurrentDirectory); // --> $(TargetDir) aka $(ProjectDir)bin\x86\Release
            var itemPath = Path.Combine(dir.FullName, filePath);
            for (int i = 0; i <= 5; i++)
            {
                if (File.Exists(itemPath))
                    return itemPath;
                if (dir.Parent == null) break;
                dir = dir.Parent;
                itemPath = Path.Combine(dir.FullName, filePath);
            }

            throw new FileNotFoundException("Could not locate file", itemPath);
        }

    }
}
