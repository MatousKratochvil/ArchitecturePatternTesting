using Eshop.Base.Abstraction.Domain;

namespace Eshop.Component.Finance.Domain
{
	internal class Payment : IAggregateRoot<int>
	{
		public int Id { get; set; }
		public bool IsDisabled { get; set; }
		public string Name { get; set; }
	}
}