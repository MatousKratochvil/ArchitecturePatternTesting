using Eshop.Base.Abstraction.Infrastructure;
using Eshop.Component.Finance.Domain;
using Eshop.Component.Finance.Infrastructure;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Eshop.Component.Finance
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

			services.AddTransient<IRepository<Payment, int>, PaymentRepository>();
		}
	}
}