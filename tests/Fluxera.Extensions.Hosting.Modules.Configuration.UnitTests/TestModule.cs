namespace Fluxera.Extensions.Hosting.Modules.Configuration.UnitTests
{
	[DependsOn(typeof(ConfigurationModule))]
	public class TestModule : ConfigureServicesModule
	{
		/// <inheritdoc />
		public override void PreConfigureServices(IServiceConfigurationContext context)
		{
			context.Services.AddConfigureOptionsContributor<TestConfigureOptionsContributor>();
		}
	}
}
