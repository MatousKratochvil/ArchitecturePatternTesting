using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Eshop.Component.LogisticsTests")] // UnitTests, IntegrationTests necessity
[assembly: InternalsVisibleTo("Eshop.Component.Performance")] // PerformanceTests necessity
[assembly: InternalsVisibleTo("DynamicProxyGenAssembly2")] // NSubstitute necessity
namespace Eshop.Component.Logistics {  }