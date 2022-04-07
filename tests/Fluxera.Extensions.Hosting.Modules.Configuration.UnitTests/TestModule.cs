namespace Fluxera.Extensions.Hosting.Modules.Configuration.UnitTests
{
	[DependsOn(typeof(ConfigurationModule))]
	public class TestModule : ConfigureServicesModule
	{
		/// <inheritdoc />
		public override void ConfigureServices(IServiceConfigurationContext context)
		{
			context.Services.AddConfigureContributor<TestConfigureContributor>();
		}
	}
}
