using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;

[assembly: AssemblyTitle("NRules")]
[assembly: AssemblyDescription("Business rules engine for .NET ")]

[assembly: CLSCompliant(true)]
//[assembly: AllowPartiallyTrustedCallers]

[assembly: InternalsVisibleTo("NRules.Tests")]
[assembly: InternalsVisibleTo("NRules.IntegrationTests")]
[assembly: InternalsVisibleTo("DynamicProxyGenAssembly2")]