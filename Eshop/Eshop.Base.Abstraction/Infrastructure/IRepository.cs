using System.Collections.Generic;
using Eshop.Base.Abstraction.Domain;

namespace Eshop.Base.Abstraction.Infrastructure
{
	/// <summary>
	/// Data persistent.
	/// </summary>
	/// <typeparam name="TDomainRoot">Domain class</typeparam>
	/// <typeparam name="TId">Domain identification type</typeparam>
	public interface IRepository<TDomainRoot, TId>
		where TDomainRoot : class, IAggregateRoot<TId>
		where TId : struct
	{
		/// <summary>
		/// Persist a domain class.
		/// </summary>
		/// <param name="domain">Domain class</param>
		/// <returns>Success of operation</returns>
		bool Save(TDomainRoot domain);

		/// <summary>
		/// Persist a domain classes
		/// </summary>
		/// <param name="domains">Collection of domain class</param>
		/// <returns>Success of operation</returns>
		bool SaveAll(IEnumerable<TDomainRoot> domains);

		/// <summary>
		/// Retrieve a domain class based on unique identification.
		/// </summary>
		/// <param name="id">Unique identification</param>
		/// <returns>Domain correspond to identification</returns>
		TDomainRoot Load(TId id);

		/// <summary>
		/// Retrieve a domain classes based on specification.
		/// </summary>
		/// <param name="specification">Search specification</param>
		/// <returns>Collection of domain classes</returns>
		IEnumerable<TDomainRoot> LoadWhere(ISpecification<TDomainRoot, TId> specification);

		/// <summary>
		/// Delete from persistent storage.
		/// </summary>
		/// <param name="domain">Domain to delete</param>
		/// <returns>Success of operation</returns>
		bool Delete(TDomainRoot domain);

		/// <summary>
		/// Delete form persistent storage.
		/// </summary>
		/// <param name="id">Unique identification</param>
		/// <returns>Success of operation</returns>
		bool Delete(TId id);
	}
}