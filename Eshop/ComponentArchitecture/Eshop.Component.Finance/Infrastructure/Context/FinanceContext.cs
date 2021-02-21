using Eshop.Component.Finance.Domain;
using Microsoft.EntityFrameworkCore;

namespace Eshop.Component.Finance.Infrastructure.Context
{
	internal class FinanceContext : DbContext
	{
		private DbSet<Payment> Payments { get; set; }
	}
}