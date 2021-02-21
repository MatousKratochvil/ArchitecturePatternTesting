using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Eshop.Base.Abstraction;
using Eshop.Base.Abstraction.Infrastructure;
using Eshop.Component.Finance.Domain;
using Eshop.Component.Finance.Specification;
using MediatR;

namespace Eshop.Component.Finance.Application
{
	public class GetPaymentsRequest : IRequest<Result<GetPaymentsResponse>>
	{
	}

	public class GetPaymentsResponse
	{
		public IReadOnlyList<PaymentItem> Data { get; set; }
	}

	public class PaymentItem
	{
		public int Id;
		public string Name;
	}

	internal class GetPaymentsHandler : IRequestHandler<GetPaymentsRequest, Result<GetPaymentsResponse>>
	{
		private readonly IRepository<Payment, int> _paymentRepository;

		public GetPaymentsHandler(IRepository<Payment, int> paymentRepository)
		{
			_paymentRepository = paymentRepository ?? throw new ArgumentNullException(nameof(paymentRepository));
		}

		public Task<Result<GetPaymentsResponse>> Handle(GetPaymentsRequest request, CancellationToken cancellationToken)
		{
			var notDisabled = new NotDisabledSpecification();
			var payments = _paymentRepository.LoadWhere(notDisabled);

			return Result<GetPaymentsResponse>
				.SuccessTask(new GetPaymentsResponse
				{
					Data = payments?.Select(x => new PaymentItem
					{
						Id = x.Id,
						Name = x.Name
					}).ToList().AsReadOnly() ?? new List<PaymentItem>().AsReadOnly()
				});
		}
	}
}