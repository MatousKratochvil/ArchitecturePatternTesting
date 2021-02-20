using Eshop.Component.Catalog.Domain;
using Eshop.Component.Catalog.Infrastructure.Context;

namespace Eshop.Component.Catalog.Infrastructure
{
	/// <inheritdoc />
	internal class CommodityRepository : BaseRepository<Commodity, long>
	{
		public CommodityRepository(CatalogContext context) : base(context)
		{
		}
	}
}