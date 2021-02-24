using System;
using System.Linq.Expressions;
using Eshop.Base.Abstraction.Infrastructure;
using Eshop.Component.Catalog.Domain;

namespace Eshop.Component.Catalog.Specification
{
	internal class CategoryIdSpecification : ISpecification<Commodity, long>
	{
		private readonly int _categoryId;

		public CategoryIdSpecification(int categoryId)
		{
			_categoryId = categoryId;
		}

		public Expression<Func<Commodity, bool>> Operation() => item => item.CategoryId == _categoryId;
	}
}