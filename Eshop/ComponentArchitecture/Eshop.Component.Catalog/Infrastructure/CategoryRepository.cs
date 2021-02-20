using Eshop.Component.Catalog.Domain;
using Eshop.Component.Catalog.Infrastructure.Context;

namespace Eshop.Component.Catalog.Infrastructure
{
	/// <inheritdoc />
	internal class CategoryRepository : BaseRepository<Category, int>
	{
		public CategoryRepository(CatalogContext context) : base(context)
		{
		}
	}
}