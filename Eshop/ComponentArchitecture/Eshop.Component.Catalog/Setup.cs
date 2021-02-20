using Eshop.Base.Abstraction.Infrastructure;
using Eshop.Component.Catalog.Domain;
using Eshop.Component.Catalog.Infrastructure;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Eshop.Component.Catalog
{
	/// <summary>
	/// Contains all necessary methods to properly create component. 
	/// </summary>
	public class Setup
	{
		/// <summary>
		/// Adds all necessary classes to Dependency Injection container
		/// </summary>
		/// <param name="services">Dependency injection services collection</param>
		public void AddComponent(IServiceCollection services)
		{
			services.AddMediatR(typeof(Setup).Assembly);
			
			services.AddTransient<IRepository<Commodity, long>, CommodityRepository>();
			services.AddTransient<IRepository<Category, int>, CategoryRepository>();

		}
	}
}