using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Eshop.Base.Abstraction.Exceptions;
using Eshop.Base.Abstraction.Infrastructure;
using Eshop.Component.Catalog.Domain;
using Eshop.Component.Catalog.Specification;
using MediatR;

namespace Eshop.Component.Catalog.Application
{
	public class GetCategoryExceptionRequest : IRequest<GetCategoryExceptionResponse>
	{
		public int CategoryId { get; set; }
	}

	public class GetCategoryExceptionResponse
	{
		public int Id { get; set; }
		public string CategoryName { get; set; }
		public IEnumerable<CategoryExceptionItem> Data { get; set; }
	}

	public class CategoryExceptionItem
	{
		public long Id { get; set; }

		public string ImageLink { get; set; }

		public string Description { get; set; }

		public string Name { get; set; }
	}

	internal class GetCategoryExceptionHandler : IRequestHandler<GetCategoryExceptionRequest, GetCategoryExceptionResponse>
	{
		private readonly IRepository<Commodity, long> _catalogItemRepository;
		private readonly IRepository<Category, int> _categoryRepository;

		public GetCategoryExceptionHandler(
			IRepository<Commodity, long> catalogItemRepository,
			IRepository<Category, int> categoryRepository)
		{
			_catalogItemRepository =
				catalogItemRepository ?? throw new ArgumentNullException(nameof(catalogItemRepository));
			_categoryRepository =
				categoryRepository ?? throw new ArgumentNullException(nameof(categoryRepository));
		}

		public Task<GetCategoryExceptionResponse> Handle(GetCategoryExceptionRequest request, CancellationToken cancellationToken)
		{
			var category = _categoryRepository.Load(request.CategoryId);
			if (category == null)
				throw new NotFoundException(nameof(Category), request.CategoryId);

			var categorySpecification = new CategoryIdSpecification(request.CategoryId);
			var catalogItems = _catalogItemRepository.LoadWhere(categorySpecification);

			return Task.FromResult(new GetCategoryExceptionResponse
				{
					Id = category.Id,
					CategoryName = category.Name,
					Data = catalogItems?.Select(commodity => new CategoryExceptionItem
					{
						Id = commodity.Id,
						Name = commodity.Name,
						Description = commodity.Description,
						ImageLink = commodity.ImageLink
					}) ?? new List<CategoryExceptionItem>()
				});
		}
	}
}