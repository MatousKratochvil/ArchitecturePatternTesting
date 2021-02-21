using Eshop.Component.Logistics.Domain;
using Microsoft.EntityFrameworkCore;

namespace Eshop.Component.Logistics.Infrastructure.Context
{
	internal class LogisticsContext : DbContext
	{
		public DbSet<PickingPoint> PickingPoints { get; set; }
	}
}