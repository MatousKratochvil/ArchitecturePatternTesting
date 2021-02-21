using System;
using System.Threading;
using System.Threading.Tasks;
using Eshop.Base.Abstraction;
using Eshop.Base.Abstraction.Exceptions;
using Eshop.Base.Abstraction.Infrastructure;
using Eshop.Component.Finance.Domain;
using MediatR;

namespace Eshop.Component.Finance.Application
{
	public class UpdatePriceCommand : IRequest<Result<bool>>
	{
		public long CommodityId { get; set; }

		public decimal Price { get; set; }
	}

	internal class UpdatePriceHandler : IRequestHandler<UpdatePriceCommand, Result<bool>>
	{
		private IRepository<Commodity, long> _commodityRepository;
		
		public UpdatePriceHandler(IRepository<Commodity, long> commodityRepository)
		{
			_commodityRepository = commodityRepository ?? throw new ArgumentNullException(nameof(commodityRepository));
		}
		
		public Task<Result<bool>> Handle(UpdatePriceCommand request, CancellationToken cancellationToken)
		{
			var commodity = _commodityRepository.Load(request.CommodityId);
			if (commodity == null)
				return Result<bool>
					.FailedTask(new NotFoundException(nameof(Commodity), request.CommodityId));

			commodity.Price = request.Price;

			var isSuccess = _commodityRepository.Save(commodity);
			if (!isSuccess)
				return Result<bool>
					.FailedTask(new UnsuccessfulUpdateException(nameof(Commodity), request.CommodityId, request.Price));

			return Result<bool>.SuccessTask(true);
		}
	}
}