using Eshop.Component.Finance.Domain;
using Eshop.Component.Finance.Infrastructure.Context;

namespace Eshop.Component.Finance.Infrastructure
{
	internal class PaymentRepository : BaseRepository<Payment, int>
	{
		public PaymentRepository(FinanceContext context) : base(context)
		{
		}
	}
}