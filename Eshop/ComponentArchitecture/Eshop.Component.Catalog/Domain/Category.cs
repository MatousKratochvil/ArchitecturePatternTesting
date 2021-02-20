using Eshop.Base.Abstraction.Domain;

namespace Eshop.Component.Catalog.Domain
{
	internal class Category : IAggregateRoot<int>
	{
		public int Id { get; set; }
		public string Name { get; set; }
	}
}