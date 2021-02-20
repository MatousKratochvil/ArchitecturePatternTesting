using System;
using System.Linq;
using System.Threading.Tasks;
using AutoFixture;
using Eshop.Base.Abstraction.Exceptions;
using Eshop.Base.Abstraction.Infrastructure;
using Eshop.Component.Catalog.Application;
using Eshop.Component.Catalog.Domain;
using FluentAssertions;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using Xunit;

namespace Eshop.Component.CatalogTests
{
	public class ApplicationsTests : IClassFixture<TestFixture>
	{
		private IRepository<Category, int> _categoryRepository =
			Substitute.For<IRepository<Category, int>>();

		private IRepository<Commodity, long> _commodityRepository =
			Substitute.For<IRepository<Commodity, long>>();

		private IFixture _fixture = new Fixture(); 

		private IServiceProvider _serviceProvider;
		
		public ApplicationsTests(TestFixture fixture)
		{
			var services = fixture.Services;

			services.AddTransient(_ => _categoryRepository);
			services.AddTransient(_ => _commodityRepository);

			_serviceProvider = services.BuildServiceProvider();
		}

		[Fact]
		public async Task GetCategory_FullCategoryResponse_ExistingCategoryAndCategoryItems()
		{
			// Arrange
			var mediator = _serviceProvider.GetService<IMediator>();
			const int commoditiesCount = 1;
			
			var category = _fixture.Create<Category>();
			var commodities = _fixture.Build<Commodity>()
				.With(x => x.CategoryId, category.Id)
				.CreateMany(commoditiesCount)
				.ToList();

			_commodityRepository.LoadWhere(Arg.Any<ISpecification<Commodity, long>>())
				.Returns(commodities);
			_categoryRepository.Load(Arg.Any<int>())
				.Returns(category);
			
			// Act
			var getCategoryResponse = await mediator.Send(new GetCategoryRequest
			{
				CategoryId = category.Id
			});
			
			// Assert
			getCategoryResponse.Should().NotBeNull();
			getCategoryResponse.Value.Should().NotBeNull();
			getCategoryResponse.Value.Id.Should().Be(category.Id);
			getCategoryResponse.Value.Data.Count().Should().Be(commoditiesCount);
			
			var firstCommodity = commodities.First();
			getCategoryResponse.Value.Data.First().Id.Should().Be(firstCommodity.Id);
			getCategoryResponse.Value.Data.First().Name.Should().Be(firstCommodity.Name);
			getCategoryResponse.Value.Data.First().ImageLink.Should().Be(firstCommodity.ImageLink);
			getCategoryResponse.Value.Data.First().Description.Should().Be(firstCommodity.Description);
			
			_categoryRepository.Received(1).Load(Arg.Any<int>());
			_commodityRepository.Received(1).LoadWhere(Arg.Any<ISpecification<Commodity, long>>());
		}

		[Fact]
		public async Task GetCategory_NullReferenceException_NonExistingCategory()
		{
			// Arrange
			var mediator = _serviceProvider.GetService<IMediator>();
			const int categoryId = 1;
			
			_commodityRepository.LoadWhere(Arg.Any<ISpecification<Commodity, long>>())
				.ReturnsNull();
			_categoryRepository.Load(Arg.Any<int>())
				.ReturnsNull();
			
			// Act
			var getCategoryResponse = await mediator.Send(new GetCategoryRequest
			{
				CategoryId = categoryId
			});

			// Assert
			getCategoryResponse.Should().NotBeNull();
			getCategoryResponse.Exception.Should().BeOfType<NotFoundException>();
			
			_categoryRepository.Received(1).Load(Arg.Any<int>());
			_commodityRepository.Received(0).LoadWhere(Arg.Any<ISpecification<Commodity, long>>());
		}
		
		[Fact]
		public async Task GetCategory_CategoryResponseWithEmptyData_ExistingCategoryButNotExistingCategoryItems()
		{
			// Arrange
			var mediator = _serviceProvider.GetService<IMediator>();

			var category = _fixture.Create<Category>();

			_commodityRepository.LoadWhere(Arg.Any<ISpecification<Commodity, long>>())
				.ReturnsNull();
			_categoryRepository.Load(Arg.Any<int>())
				.Returns(category);
			
			// Act
			var getCategoryResponse = await mediator.Send(new GetCategoryRequest
			{
				CategoryId = category.Id
			});
			
			// Assert
			getCategoryResponse.Should().NotBeNull();
			getCategoryResponse.Value.Should().NotBeNull();
			getCategoryResponse.Value.Id.Should().Be(category.Id);
			getCategoryResponse.Value.Data.Should().BeEmpty();
			
			_categoryRepository.Received(1).Load(Arg.Any<int>());
			_commodityRepository.Received(1).LoadWhere(Arg.Any<ISpecification<Commodity, long>>());
		}
	}
}