using Eshop.Component.Logistics.Domain;
using Eshop.Component.Logistics.Infrastructure.Context;

namespace Eshop.Component.Logistics.Infrastructure
{
	internal class PickingPointRepository : BaseRepository<PickingPoint, int>
	{
		public PickingPointRepository(LogisticsContext context) : base(context)
		{
		}
	}
}