using Eshop.Base.Abstraction.Domain;

namespace Eshop.Component.Logistics.Domain
{
	internal class PickingPoint : IAggregateRoot<int>
	{
		public int Id { get; set; }
	}
}