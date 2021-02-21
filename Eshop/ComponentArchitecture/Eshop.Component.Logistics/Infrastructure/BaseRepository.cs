using System;
using System.Collections.Generic;
using System.Linq;
using Eshop.Base.Abstraction.Domain;
using Eshop.Base.Abstraction.Infrastructure;
using Eshop.Component.Logistics.Infrastructure.Context;

namespace Eshop.Component.Logistics.Infrastructure
{
	/// <inheritdoc />
	internal abstract class BaseRepository<TDomain, TId> : IRepository<TDomain, TId>
		where TDomain : class, IAggregateRoot<TId>
		where TId : struct
	{
		/// <summary>
		/// Finance context EF core.
		/// </summary>
		protected readonly LogisticsContext _context;
		
		public BaseRepository(LogisticsContext context)
		{
			_context = context ?? throw new ArgumentNullException(nameof(context));
		}
		public bool Save(TDomain domain)
		{
			_context.Add(domain);
			return _context.SaveChanges() > 0;
		}

		/// <inheritdoc />
		public bool SaveAll(IEnumerable<TDomain> domains)
		{
			_context.AddRange(domains);
			return _context.SaveChanges() > 0;
		}

		/// <inheritdoc />
		public TDomain Load(TId id)
		{
			return _context.Find<TDomain>(id);
		}

		/// <inheritdoc />
		public IEnumerable<TDomain> LoadWhere(ISpecification<TDomain, TId> specification)
		{
			return _context.Set<TDomain>().Where(specification.Operation());
		}

		/// <inheritdoc />
		public bool Delete(TDomain domain)
		{
			_context.Remove(domain);
			return _context.SaveChanges() > 0;
		}

		/// <inheritdoc />
		public bool Delete(TId id)
		{
			var domain = _context.Find<TDomain>(id);
			_context.Set<TDomain>().Remove(domain);
			return _context.SaveChanges() > 0;
		}
	}
}