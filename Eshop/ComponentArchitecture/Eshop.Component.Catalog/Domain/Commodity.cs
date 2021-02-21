using Eshop.Base.Abstraction.Domain;

namespace Eshop.Component.Catalog.Domain
{
	internal class Commodity : IAggregateRoot<long>
	{
		public long Id { get; set; }

		public Category Category { get; set; }
		public int CategoryId { get; set; }

		public string ImageLink { get; set; }

		public string Description { get; set; }

		public string Name { get; set; }
		
		public decimal Price { get; set; }
	}
}