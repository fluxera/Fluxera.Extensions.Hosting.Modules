namespace Ordering.HttpClient
{
	using Fluxera.Extensions.Hosting;
	using Fluxera.Extensions.Hosting.Modules;
	using Fluxera.Extensions.Hosting.Modules.HttpClient;
	using JetBrains.Annotations;
	using Ordering.HttpClient.Contributors;

	[PublicAPI]
	[DependsOn<HttpClientModule>]
	public sealed class OrderingHttpClientModule : ConfigureApplicationModule
	{
		/// <inheritdoc />
		public override void PreConfigureServices(IServiceConfigurationContext context)
		{
			// Add the http client service contributor.
			context.Services.AddHttpClientServiceContributor<HttpClientServiceContributor>();
		}
	}
}
