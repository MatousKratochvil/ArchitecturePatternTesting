namespace Eshop.Component.Performance
{
	class Program
	{
		static void Main(string[] args)
		{
			BenchmarkDotNet.Running.BenchmarkRunner.Run<ApplicationPerformanceUnitTest>();
		}
	}
}