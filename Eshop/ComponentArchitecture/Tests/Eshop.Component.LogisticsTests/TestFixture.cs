using Eshop.Component.Logistics;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Eshop.Component.LogisticsTests
{
	public class TestFixture
	{
		public IServiceCollection Services; 
		
		public TestFixture()
		{
			var services = new ServiceCollection();

			// Loading ALL handlers (probably not ideal for unit tests)
			services.AddMediatR(typeof(Setup).Assembly);

			Services = services;
		}
	}
}