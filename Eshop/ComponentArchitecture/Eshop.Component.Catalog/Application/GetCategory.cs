using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Eshop.Base.Abstraction;
using Eshop.Base.Abstraction.Exceptions;
using Eshop.Base.Abstraction.Infrastructure;
using Eshop.Component.Catalog.Domain;
using Eshop.Component.Catalog.Specification;
using MediatR;

namespace Eshop.Component.Catalog.Application
{
	public class GetCategoryRequest : IRequest<Result<GetCategoryResponse>>
	{
		public int CategoryId { get; set; }
	}

	public class GetCategoryResponse
	{
		public int Id { get; set; }
		public string CategoryName { get; set; }
		public IReadOnlyList<CategoryItem> Data { get; set; }
	}

	public class CategoryItem
	{
		public long Id { get; set; }

		public string ImageLink { get; set; }

		public string Description { get; set; }

		public string Name { get; set; }
	}

	internal class GetCategoryHandler : IRequestHandler<GetCategoryRequest, Result<GetCategoryResponse>>
	{
		private readonly IRepository<Commodity, long> _catalogItemRepository;
		private readonly IRepository<Category, int> _categoryRepository;

		public GetCategoryHandler(
			IRepository<Commodity, long> catalogItemRepository,
			IRepository<Category, int> categoryRepository)
		{
			_catalogItemRepository =
				catalogItemRepository ?? throw new ArgumentNullException(nameof(catalogItemRepository));
			_categoryRepository =
				categoryRepository ?? throw new ArgumentNullException(nameof(categoryRepository));
		}

		public Task<Result<GetCategoryResponse>> Handle(GetCategoryRequest request, CancellationToken cancellationToken)
		{
			var category = _categoryRepository.Load(request.CategoryId);
			if (category == null)
				return Result<GetCategoryResponse>
					.FailedTask(new NotFoundException(nameof(Category), request.CategoryId));

			var categorySpecification = new CategoryIdSpecification(request.CategoryId);
			var catalogItems = _catalogItemRepository.LoadWhere(categorySpecification);

			return Result<GetCategoryResponse>
				.SuccessTask(new GetCategoryResponse
				{
					Id = category.Id,
					CategoryName = category.Name,
					Data = catalogItems?.Select(commodity => new CategoryItem
					{
						Id = commodity.Id,
						Name = commodity.Name,
						Description = commodity.Description,
						ImageLink = commodity.ImageLink
					}).ToList().AsReadOnly() ?? new List<CategoryItem>().AsReadOnly()
				});
		}
	}
}