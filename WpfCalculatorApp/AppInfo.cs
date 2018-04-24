using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Reflection;
using NEdifis.Attributes;

namespace WpfCalculatorApp
{
    // cf.: https://gist.github.com/mkoertgen/33f8b90050599c3c3335

    [ExcludeFromCodeCoverage]
    [ExcludeFromConventions("This was a minimod")]
    [Because("This was a minimod")]
    public class AppInfo
    {
        private static AppInfo _default;

        public static AppInfo Default => _default ?? (_default = new AppInfo(Assembly.GetEntryAssembly() ?? Assembly.GetExecutingAssembly()));

        public Assembly Assembly { get; }
        public string Product { get; }
        public string Title { get; }
        public string Description { get; }
        public string Version { get; }
        public string SystemInfo { get; }
        public DateTime BuildDate { get; }
        public string Copyright { get; }
        public string Company { get; }

        private AppInfo(Assembly assembly)
        {
            if (assembly == null) throw new ArgumentNullException(nameof(assembly));
            Assembly = assembly;

            Product = " ";
            Company = Assembly.GetCompany();
            Title = "Frequenzmanagement";
            Version = GetProductVersion();
            BuildDate = GetBuildDate(Assembly.Location);
            Copyright = Assembly.GetCopyright();
            SystemInfo = GetSystemInfo();
            Description = Assembly.GetDescription();
        }

        // ReSharper disable once MemberCanBeMadeStatic.Local
        private string GetProductVersion()
        {
#if NCRUNCH
            Console.WriteLine(Assembly.GetProductVersion());
            return Assembly.GetProductVersion();
#else
            return ModeDetector.IsNcrunch || ModeDetector.IsResharper
                ? Assembly.GetProductVersion()
                : GitVersionInformation.SemVer;
#endif
        }

        private static DateTime GetBuildDate(string filePath)
        {
            // NOTE: last write time is not an option
            // cf: http://stackoverflow.com/questions/1600962/displaying-the-build-date
            const int peHeaderOffset = 60;
            const int linkerTimestampOffset = 8;
            var header = new byte[2048];
            using (var stream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                stream.Read(header, 0, 2048);

            var offset = BitConverter.ToInt32(header, peHeaderOffset);
            var secondsSince1970 = BitConverter.ToInt32(header, offset + linkerTimestampOffset);
            var buildDate = new DateTime(1970, 1, 1, 0, 0, 0);
            buildDate = buildDate.AddSeconds(secondsSince1970);
            buildDate = buildDate.AddHours(TimeZone.CurrentTimeZone.GetUtcOffset(buildDate).Hours);
            return buildDate;
        }

        private string GetSystemInfo()
        {
            string bitness;
            switch (IntPtr.Size)
            {
                case 4: bitness = " (32-bit)"; break;
                case 8: bitness = " (64-bit)"; break;
                default: bitness = string.Empty; break;
            }

            return $"{Environment.OSVersion}, .NET {Assembly.ImageRuntimeVersion.Substring(0, 4)}{bitness}";
        }
    }
}
