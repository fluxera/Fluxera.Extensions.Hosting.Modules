namespace Ordering.HttpClient.Services
{
	using System.Net.Http;
	using Fluxera.Extensions.Http;
	using JetBrains.Annotations;

	[UsedImplicitly]
	internal sealed class OrderingHttpClient : HttpClientServiceBase, IOrderingHttpClient
	{
		/// <inheritdoc />
		public OrderingHttpClient(string name, HttpClient httpClient, RemoteService options)
			: base(name, httpClient, options)
		{
		}
	}
}
