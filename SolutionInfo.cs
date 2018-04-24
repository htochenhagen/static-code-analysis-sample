using System.Runtime.CompilerServices;
using System.Reflection;
using System.Resources;
using System.Runtime.InteropServices;

#if DEBUG
[assembly: AssemblyConfiguration("Debug")]
#else
[assembly: AssemblyConfiguration("Release")]
#endif

[assembly: AssemblyDescription(@"© Schönhofer Sales and Engineering GmbH 2007 – 2018.
Schutzvermerk ISO 16016 beachten.
Nutzung nur im Rahmen der Regelungen eines wirksam geschlossenen Nutzungsvertrags.
Software shall only be run in compliance with the respective contractual terms and conditions.")]
[assembly: AssemblyCompany("Schönhofer")]
[assembly: AssemblyProduct("WpfCalculatorApp")]
[assembly: AssemblyCopyright("© Schönhofer Sales and Engineering GmbH 2018")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]
[assembly: ComVisible(false)]

[assembly: NeutralResourcesLanguage("en", UltimateResourceFallbackLocation.MainAssembly)]

[assembly: InternalsVisibleTo("WpfCalculatorApp")]
[assembly: InternalsVisibleTo("Requirements")]
