namespace Fluxera.Extensions.Hosting.Modules.ODataClient
{
	using Fluxera.Extensions.Hosting.Modules.Configuration;
	using Fluxera.Extensions.Hosting.Modules.HttpClient;
	using Fluxera.Extensions.Hosting.Modules.ODataClient.Contributors;
	using JetBrains.Annotations;

	/// <summary>
	///     A module that enables the OData client.
	/// </summary>
	[PublicAPI]
	[DependsOn(typeof(HttpClientModule))]
	public sealed class ODataClientModule : ConfigureServicesModule
	{
		/// <inheritdoc />
		public override void PreConfigureServices(IServiceConfigurationContext context)
		{
			// Add the configure options contributor.
			context.Services.AddConfigureOptionsContributor<ConfigureOptionsContributor>();
		}
	}
}
