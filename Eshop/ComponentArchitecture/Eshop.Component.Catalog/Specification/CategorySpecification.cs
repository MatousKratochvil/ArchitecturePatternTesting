using System;
using Eshop.Base.Abstraction.Infrastructure;
using Eshop.Component.Catalog.Domain;

namespace Eshop.Component.Catalog.Specification
{
	internal class CategorySpecification : ISpecification<Commodity, long>
	{
		private readonly int _categoryId;

		public CategorySpecification(int categoryId)
		{
			_categoryId = categoryId;
		}

		public Func<Commodity, bool> Operation() => item => item.CategoryId == _categoryId;
	}
}