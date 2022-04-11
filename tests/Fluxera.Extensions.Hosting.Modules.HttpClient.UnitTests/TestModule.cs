namespace Fluxera.Extensions.Hosting.Modules.HttpClient.UnitTests
{
	[DependsOn(typeof(HttpClientModule))]
	public class TestModule : ConfigureServicesModule
	{
		/// <inheritdoc />
		public override void PreConfigureServices(IServiceConfigurationContext context)
		{
			context.Services.AddHttpClientServiceContributor<TestHttpClientServiceContributor>();
		}
	}
}
