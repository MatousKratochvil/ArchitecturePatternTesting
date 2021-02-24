using System;
using System.Linq.Expressions;
using Eshop.Base.Abstraction.Infrastructure;
using Eshop.Component.Finance.Domain;

namespace Eshop.Component.Finance.Specification
{
	internal class NotDisabledSpecification : ISpecification<Payment, int>
	{
		public Expression<Func<Payment, bool>> Operation() => payment => !payment.IsDisabled;
	}
}