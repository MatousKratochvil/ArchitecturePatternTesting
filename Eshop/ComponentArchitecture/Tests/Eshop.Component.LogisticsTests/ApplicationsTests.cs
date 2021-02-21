using System;
using AutoFixture;
using Eshop.Base.Abstraction.Infrastructure;
using Eshop.Component.Logistics.Domain;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using Xunit;

namespace Eshop.Component.LogisticsTests
{
	public class ApplicationsTests : IClassFixture<TestFixture>
	{
		private IRepository<PickingPoint, int> _paymentRepository =
			Substitute.For<IRepository<PickingPoint, int>>();

		private IFixture _fixture = new Fixture();

		private IServiceProvider _serviceProvider;

		public ApplicationsTests(TestFixture fixture)
		{
			var services = fixture.Services;

			services.AddTransient(_ => _paymentRepository);
			
			_serviceProvider = services.BuildServiceProvider();
		}
	}
}