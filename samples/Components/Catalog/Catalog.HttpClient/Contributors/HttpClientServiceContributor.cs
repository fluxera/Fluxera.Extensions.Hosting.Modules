namespace Catalog.HttpClient.Contributors
{
	using Catalog.Application.Contracts.Services;
	using Catalog.HttpClient.Services;
	using Fluxera.Extensions.Hosting;
	using Fluxera.Extensions.Hosting.Modules.HttpClient;
	using Fluxera.Extensions.Http;
	using Microsoft.Extensions.DependencyInjection;
	using System.Collections.Generic;

	internal sealed class HttpClientServiceContributor : IHttpClientServiceContributor
	{
		/// <inheritdoc />
		public IEnumerable<IHttpClientBuilder> AddNamedHttpClientServices(IServiceConfigurationContext context)
		{
			yield return context.Services.AddHttpClientService<IProductApplicationService, ProductApplicationServiceClient>(
				"Catalog",
				(ctx, _) =>
				{
					ProductApplicationServiceClient client = new ProductApplicationServiceClient(ctx.Name, ctx.HttpClient, ctx.Options);
					return client;
				});
		}
	}
}
