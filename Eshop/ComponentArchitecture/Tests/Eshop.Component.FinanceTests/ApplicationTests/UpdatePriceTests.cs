using System;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using AutoFixture;
using Eshop.Base.Abstraction.Infrastructure;
using Eshop.Component.Finance.Application;
using Eshop.Component.Finance.Domain;
using FluentAssertions;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using Xunit;

namespace Eshop.Component.FinanceTests.ApplicationTests
{
	public class UpdatePriceTests : IClassFixture<TestFixture>
	{
		private IRepository<Commodity, long> _commodityRepository =
			Substitute.For<IRepository<Commodity, long>>();

		private IFixture _fixture = new Fixture();

		private IServiceProvider _serviceProvider;

		public UpdatePriceTests(TestFixture fixture)
		{
			var services = fixture.Services;

			services.AddTransient(_ => _commodityRepository);

			_serviceProvider = services.BuildServiceProvider();
		}

		[Fact]
		public async Task UpdatePrice_SuccessfulResponse_ValidDataExistingCommodity()
		{
			// Arrange
			var mediator = _serviceProvider.GetService<IMediator>();
			const decimal commodityPriceAfterUpdate = decimal.One;

			var commodity = _fixture
				.Build<Commodity>()
				.With(x => x.Price, decimal.Zero)
				.Create();

			_commodityRepository.Load(Arg.Is(commodity.Id))
				.Returns(commodity);
			_commodityRepository.Save(Arg.Any<Commodity>())
				.Returns(true);

			// Act
			var getCategoryResponse = await mediator.Send(new UpdatePriceCommand
			{
				Price = commodityPriceAfterUpdate,
				CommodityId = commodity.Id
			});

			// Assert
			getCategoryResponse.Should().NotBeNull();
			getCategoryResponse.Value.Should().BeTrue();
			
			commodity.Price.Should().Be(commodityPriceAfterUpdate);
			
			_commodityRepository.Received(1).Load(Arg.Any<long>());
			_commodityRepository.Received(1).Save(Arg.Any<Commodity>());
		}
	}
}