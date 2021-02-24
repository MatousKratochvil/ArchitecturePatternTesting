using System;
using System.Linq.Expressions;
using Eshop.Base.Abstraction.Domain;

namespace Eshop.Base.Abstraction.Infrastructure
{
	/// <summary>
	/// Specification for domain.
	/// </summary>
	/// <typeparam name="TDomainRoot">Domain class</typeparam>
	/// <typeparam name="TId">Domain identification type</typeparam>
	public interface ISpecification<TDomainRoot, in TId>
		where TDomainRoot : class, IAggregateRoot<TId>
		where TId : struct
	{
		/// <summary>
		/// Specification for domain selection.
		/// </summary>
		/// <returns>Selection function (Where func)</returns>
		Expression<Func<TDomainRoot, bool>> Operation();
	}
}