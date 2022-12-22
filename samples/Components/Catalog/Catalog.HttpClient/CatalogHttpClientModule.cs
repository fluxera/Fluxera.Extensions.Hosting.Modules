namespace Catalog.HttpClient
{
	using Catalog.HttpClient.Contributors;
	using Fluxera.Extensions.Hosting;
	using Fluxera.Extensions.Hosting.Modules;
	using Fluxera.Extensions.Hosting.Modules.Configuration;
	using Fluxera.Extensions.Hosting.Modules.HttpClient;
	using JetBrains.Annotations;

	[PublicAPI]
	[DependsOn<HttpClientModule>]
	[DependsOn<ConfigurationModule>]
	public sealed class CatalogHttpClientModule : ConfigureApplicationModule
	{
		/// <inheritdoc />
		public override void PreConfigureServices(IServiceConfigurationContext context)
		{
			// Add the http client service contributor.
			context.Services.AddHttpClientServiceContributor<HttpClientServiceContributor>();
		}
	}
}
