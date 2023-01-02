namespace Ordering.HttpClient.Contributors
{
	using System.Collections.Generic;
	using Fluxera.Extensions.Hosting;
	using Fluxera.Extensions.Hosting.Modules.HttpClient;
	using Fluxera.Extensions.Http;
	using Microsoft.Extensions.DependencyInjection;
	using Ordering.Application.Contracts.Orders;
	using Ordering.HttpClient.Services;

	internal sealed class HttpClientServiceContributor : IHttpClientServiceContributor
	{
		/// <inheritdoc />
		public IEnumerable<IHttpClientBuilder> AddNamedHttpClientServices(IServiceConfigurationContext context)
		{
			yield return context.Services.AddHttpClientService<IOrderApplicationService, OrderApplicationServiceClient>(
				"Ordering",
				(ctx, _) =>
				{
					OrderApplicationServiceClient client = new OrderApplicationServiceClient(ctx.Name, ctx.HttpClient, ctx.Options);
					return client;
				});
		}
	}
}
