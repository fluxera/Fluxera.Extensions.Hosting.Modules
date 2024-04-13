namespace Ordering.Application.Orders
{
	using System.Threading.Tasks;
	using Fluxera.Extensions.Hosting.Modules.Application.Contracts.Dtos;
	using JetBrains.Annotations;
	using MediatR;
	using Ordering.Application.Contracts.Orders;

	[UsedImplicitly]
	internal sealed class OrderApplicationService : IOrderApplicationService
	{
		private readonly IMediator mediator;

		public OrderApplicationService(IMediator mediator)
		{
			this.mediator = mediator;
		}

		/// <inheritdoc />
		public async Task<ResultDto<OrderDto[]>> GetOrdersAsync()
		{
			return ResultDto<OrderDto[]>.Ok([]);
		}
	}
}
