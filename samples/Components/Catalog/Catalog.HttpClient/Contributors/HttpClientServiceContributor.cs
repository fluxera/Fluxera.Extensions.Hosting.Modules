﻿namespace Catalog.HttpClient.Contributors
{
	using Catalog.HttpClient.Services;
	using Fluxera.Extensions.Hosting;
	using Fluxera.Extensions.Hosting.Modules.HttpClient;
	using Fluxera.Extensions.Http;
	using Microsoft.Extensions.DependencyInjection;

	internal sealed class HttpClientServiceContributor : IHttpClientServiceContributor
	{
		/// <inheritdoc />
		public IHttpClientBuilder AddNamedHttpClientServices(IServiceConfigurationContext context)
		{
			IHttpClientBuilder httpClientBuilder = context.Services.AddHttpClientService<ICatalogHttpClient, CatalogHttpClient>(
				(ctx, _) =>
				{
					CatalogHttpClient client = new CatalogHttpClient(ctx.Name, ctx.HttpClient, ctx.Options);
					return client;
				});

			return httpClientBuilder;
		}
	}
}