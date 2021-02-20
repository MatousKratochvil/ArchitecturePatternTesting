using Eshop.Component.Catalog.Domain;
using Microsoft.EntityFrameworkCore;

namespace Eshop.Component.Catalog.Infrastructure.Context
{
	internal class CatalogContext : DbContext
	{
		public DbSet<Commodity> Commodities { get; set; }
		public DbSet<Category> Categories { get; set; }
	}
}