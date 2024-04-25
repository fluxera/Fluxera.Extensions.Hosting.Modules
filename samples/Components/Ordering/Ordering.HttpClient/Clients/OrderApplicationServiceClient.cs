namespace Ordering.HttpClient.Clients
{
	using System.Collections.Generic;
	using System.Linq;
	using System.Net.Http;
	using System.Threading.Tasks;
	using Fluxera.Extensions.Hosting.Modules.Application.Contracts.Dtos.Results;
	using Fluxera.Extensions.Http;
	using JetBrains.Annotations;
	using Ordering.Application.Contracts.Orders;
	using Ordering.Domain.Shared.Orders;

	[UsedImplicitly]
	internal sealed class OrderApplicationServiceClient : HttpClientServiceBase, IOrderApplicationService, IHttpClientService
	{
		private readonly IList<OrderDto> orders = new List<OrderDto>
		{
			new OrderDto
			{
				ID = new OrderId("38835805a29042499132b635618c37dd"),
				OrderNumber = "A123456790"
			},
			new OrderDto
			{
				ID = new OrderId("a48aa043ef8d485491f27e4accdbbb15"),
				OrderNumber = "A123456789"
			},
			new OrderDto
			{
				ID = new OrderId("f802e598a1fb47a2975b546a2bd935f1"),
				OrderNumber = "A123456788"
			},
			new OrderDto
			{
				ID = new OrderId("86f059a6f20c46a9a36dcfa687d8a1c1"),
				OrderNumber = "A123456787"
			}
		};

		/// <inheritdoc />
		public OrderApplicationServiceClient(string name, HttpClient httpClient, RemoteService options)
			: base(name, httpClient, options)
		{
		}

		/// <inheritdoc />
		public Task<ResultDto<OrderDto[]>> GetOrdersAsync()
		{
			ResultDto<OrderDto[]> resultDto = new ResultDto<OrderDto[]>(this.orders.ToArray());

			return Task.FromResult(resultDto);
		}
	}
}
