namespace Ordering.HttpClient.Clients
{
	using System.Net.Http;
	using Fluxera.Extensions.Http;
	using JetBrains.Annotations;
	using Ordering.Application.Contracts.Orders;

	[UsedImplicitly]
	internal sealed class OrderApplicationServiceClient : HttpClientServiceBase, IOrderApplicationService, IHttpClientService
	{
		/// <inheritdoc />
		public OrderApplicationServiceClient(string name, HttpClient httpClient, RemoteService options)
			: base(name, httpClient, options)
		{
		}
	}
}
