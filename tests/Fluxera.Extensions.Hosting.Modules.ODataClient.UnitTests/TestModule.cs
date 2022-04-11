namespace Fluxera.Extensions.Hosting.Modules.ODataClient.UnitTests
{
	using Fluxera.Extensions.Hosting.Modules.HttpClient;

	[DependsOn(typeof(ODataClientModule))]
	public class TestModule : ConfigureServicesModule
	{
		/// <inheritdoc />
		public override void PreConfigureServices(IServiceConfigurationContext context)
		{
			context.Services.AddHttpClientServiceContributor<TestHttpClientServiceContributor>();
		}
	}
}
