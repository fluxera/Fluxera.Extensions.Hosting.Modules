namespace Ordering.HttpClient.Contributors
{
	using Fluxera.Extensions.Hosting;
	using Fluxera.Extensions.Hosting.Modules.HttpClient;
	using Fluxera.Extensions.Http;
	using Microsoft.Extensions.DependencyInjection;
	using Ordering.HttpClient.Services;

	internal sealed class HttpClientServiceContributor : IHttpClientServiceContributor
	{
		/// <inheritdoc />
		public IHttpClientBuilder AddNamedHttpClientServices(IServiceConfigurationContext context)
		{
			IHttpClientBuilder httpClientBuilder = context.Services.AddHttpClientService<IOrderingHttpClient, OrderingHttpClient>(
				(ctx, _) =>
				{
					OrderingHttpClient client = new OrderingHttpClient(ctx.Name, ctx.HttpClient, ctx.Options);
					return client;
				});

			return httpClientBuilder;
		}
	}
}
