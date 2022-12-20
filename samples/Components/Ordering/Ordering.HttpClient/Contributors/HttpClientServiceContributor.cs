namespace Ordering.HttpClient.Contributors
{
	using Fluxera.Extensions.Hosting;
	using Fluxera.Extensions.Hosting.Modules.HttpClient;
	using Fluxera.Extensions.Http;
	using Microsoft.Extensions.DependencyInjection;
	using Ordering.Application.Contracts.Services;
	using Ordering.HttpClient.Services;

	internal sealed class HttpClientServiceContributor : IHttpClientServiceContributor
	{
		/// <inheritdoc />
		public IHttpClientBuilder AddNamedHttpClientServices(IServiceConfigurationContext context)
		{
			IHttpClientBuilder httpClientBuilder = context.Services.AddHttpClientService<IOrderApplicationService, OrderApplicationServiceClient>(
				(ctx, _) =>
				{
					OrderApplicationServiceClient client = new OrderApplicationServiceClient(ctx.Name, ctx.HttpClient, ctx.Options);
					return client;
				});

			return httpClientBuilder;
		}
	}
}
