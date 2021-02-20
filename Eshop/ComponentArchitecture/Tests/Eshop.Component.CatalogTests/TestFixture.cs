using Eshop.Component.Catalog;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Eshop.Component.CatalogTests
{
	public class TestFixture
	{
		public IServiceCollection Services; 
		
		public TestFixture()
		{
			var services = new ServiceCollection();

			services.AddMediatR(typeof(Setup).Assembly);

			Services = services;
		}
	}
}