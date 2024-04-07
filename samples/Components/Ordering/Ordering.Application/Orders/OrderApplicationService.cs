namespace Ordering.Application.Orders
{
	using System;
	using System.Linq.Expressions;
	using System.Threading.Tasks;
	using Fluxera.Extensions.Hosting.Modules.Application.Contracts.Dtos;
	using JetBrains.Annotations;
	using MediatR;
	using Ordering.Application.Contracts.Orders;

	[UsedImplicitly]
	internal sealed class OrderApplicationService : IOrderApplicationService
	{
		private readonly ISender sender;

		//private readonly IList<OrderDto> orders = new List<OrderDto>
		//{
		//	new OrderDto
		//	{
		//		ID = new OrderId("38835805a29042499132b635618c37dd"),
		//		OrderNumber = "A123456790"
		//	},
		//	new OrderDto
		//	{
		//		ID = new OrderId("a48aa043ef8d485491f27e4accdbbb15"),
		//		OrderNumber = "A123456789"
		//	},
		//	new OrderDto
		//	{
		//		ID = new OrderId("f802e598a1fb47a2975b546a2bd935f1"),
		//		OrderNumber = "A123456788"
		//	},
		//	new OrderDto
		//	{
		//		ID = new OrderId("86f059a6f20c46a9a36dcfa687d8a1c1"),
		//		OrderNumber = "A123456787"
		//	}
		//};

		public OrderApplicationService(ISender sender)
		{
			this.sender = sender;
		}

		/// <inheritdoc />
		public async Task<ResultDto<OrderDto[]>> GetOrdersAsync(Expression<Func<OrderDto, bool>> predicate)
		{
			return ResultDto<OrderDto[]>.Ok([]);
		}
	}
}
