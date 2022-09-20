namespace Example.HttpClient.Contributors
{
	using Example.Application.Contracts.Services;
	using Example.HttpClient.Services;
	using Fluxera.Extensions.Hosting;
	using Fluxera.Extensions.Hosting.Modules.HttpClient;
	using Fluxera.Extensions.Http;
	using Microsoft.Extensions.DependencyInjection;

	internal sealed class HttpClientServiceContributor : IHttpClientServiceContributor
	{
		/// <inheritdoc />
		public IHttpClientBuilder AddNamedHttpClientServices(IServiceConfigurationContext context)
		{
			IHttpClientBuilder httpClientBuilder = context.Services.AddHttpClientService<IExampleApplicationService, ExampleApplicationServiceClient>(
				(ctx, sp) =>
				{
					ExampleApplicationServiceClient client = new ExampleApplicationServiceClient(ctx.Name, ctx.HttpClient, ctx.Options);
					return client;
				});

			return httpClientBuilder;
		}
	}
}
