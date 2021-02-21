using System;
using System.Linq;
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

namespace Eshop.Component.CatalogTests
{
	public class ApplicationsTests : IClassFixture<TestFixture>
	{
		private IRepository<Payment, int> _paymentRepository =
			Substitute.For<IRepository<Payment, int>>();

		private IFixture _fixture = new Fixture(); 

		private IServiceProvider _serviceProvider;
		
		public ApplicationsTests(TestFixture fixture)
		{
			var services = fixture.Services;

			services.AddTransient(_ => _paymentRepository);

			_serviceProvider = services.BuildServiceProvider();
		}

		[Fact]
		public async Task GetPayment_FullPaymentResponse_ExistingPayments()
		{
			// Arrange
			var mediator = _serviceProvider.GetService<IMediator>();
			const int paymentsCount = 1;
			
			var payments = _fixture.CreateMany<Payment>(paymentsCount).ToList();

			_paymentRepository.LoadWhere(Arg.Any<ISpecification<Payment, int>>())
				.Returns(payments);
			
			// Act
			var getCategoryResponse = await mediator.Send(new GetPaymentsRequest());
			
			// Assert
			getCategoryResponse.Should().NotBeNull();
			getCategoryResponse.Value.Should().NotBeNull();

			var firstPayment = payments.First();
			getCategoryResponse.Value.Data.First().Id.Should().Be(firstPayment.Id);
			getCategoryResponse.Value.Data.First().Name.Should().Be(firstPayment.Name);
			
			_paymentRepository.Received(1).LoadWhere(Arg.Any<ISpecification<Payment, int>>());
		}
	}
}