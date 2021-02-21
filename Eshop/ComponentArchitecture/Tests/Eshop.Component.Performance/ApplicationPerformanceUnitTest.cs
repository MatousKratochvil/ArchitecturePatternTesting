using System;
using System.Linq;
using System.Threading.Tasks;
using AutoFixture;
using BenchmarkDotNet.Attributes;
using Eshop.Base.Abstraction.Infrastructure;
using Eshop.Component.Catalog;
using Eshop.Component.Catalog.Application;
using Eshop.Component.Catalog.Domain;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using NSubstitute.ReturnsExtensions;

namespace Eshop.Component.Performance
{
	[RyuJitX64Job]
	[MemoryDiagnoser]
	[MarkdownExporter]
	public class ApplicationPerformanceUnitTest
	{
		[GlobalSetup]
		public void GlobalSetup()
		{
			var services = new ServiceCollection();

			services.AddMediatR(typeof(Setup).Assembly);
			services.AddTransient(_ => _categoryRepository);
			services.AddTransient(_ => _commodityRepository);

			_serviceProvider = services.BuildServiceProvider();

			var category = _fixture.Create<Category>();
			var commodities = _fixture.Build<Commodity>()
				.With(x => x.CategoryId, category.Id)
				.CreateMany(20)
				.ToList();

			_commodityRepository.LoadWhere(Arg.Any<ISpecification<Commodity, long>>())
				.Returns(commodities);
			_categoryRepository.Load(Arg.Any<int>())
				.ReturnsNull();
		}

		private IRepository<Category, int> _categoryRepository =
			Substitute.For<IRepository<Category, int>>();

		private IRepository<Commodity, long> _commodityRepository =
			Substitute.For<IRepository<Commodity, long>>();

		private IFixture _fixture = new Fixture();

		private IServiceProvider _serviceProvider;
		
		[Benchmark]
		public async Task GetCategory_ResultReturn()
		{
			var mediator = _serviceProvider.GetService<IMediator>();

			var getCategoryResponse = await mediator.Send(new GetCategoryRequest
			{
				CategoryId = 1
			});
		}
	}
}