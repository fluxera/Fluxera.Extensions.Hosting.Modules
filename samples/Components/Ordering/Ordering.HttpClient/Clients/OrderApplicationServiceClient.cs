namespace Ordering.HttpClient.Clients
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Linq.Expressions;
	using System.Net.Http;
	using System.Threading.Tasks;
	using Fluxera.Extensions.Hosting.Modules.Application.Contracts.Dtos;
	using Fluxera.Extensions.Hosting.Modules.ODataClient;
	using Fluxera.Extensions.Http;
	using JetBrains.Annotations;
	using Ordering.Application.Contracts.Orders;
	using Ordering.Domain.Shared.OrderAggregate;

	[UsedImplicitly]
	internal sealed class OrderApplicationServiceClient : HttpClientServiceBase, IOrderApplicationService, IHttpClientService
	{
		private readonly ODataClientHelper<OrderDto, OrderId> clientHelper;

		/// <inheritdoc />
		public OrderApplicationServiceClient(string name, HttpClient httpClient, RemoteService options)
			: base(name, httpClient, options)
		{
			this.clientHelper = new ODataClientHelper<OrderDto, OrderId>(null, null);
		}

		/// <inheritdoc />
		public async Task<ResultDto<OrderDto[]>> GetOrdersAsync(Expression<Func<OrderDto, bool>> predicate)
		{
			IReadOnlyCollection<OrderDto> orderDtos = await this.clientHelper.FindManyAsync(predicate);
			return ResultDto<OrderDto[]>.Ok(orderDtos.ToArray());
		}
	}
}
