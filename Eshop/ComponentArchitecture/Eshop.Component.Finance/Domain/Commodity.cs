using Eshop.Base.Abstraction.Domain;

namespace Eshop.Component.Finance.Domain
{
	internal class Commodity : IAggregateRoot<long>
	{
		public long Id { get; set; }

		public decimal Price { get; set; }
	}
}