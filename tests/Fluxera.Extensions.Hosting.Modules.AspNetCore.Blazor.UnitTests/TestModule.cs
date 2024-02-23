namespace Fluxera.Extensions.Hosting.Modules.AspNetCore.Blazor.UnitTests
{
	using Fluxera.Extensions.Hosting.Modules.Configuration;

	[DependsOn<BlazorModule>]
	[DependsOn<ConfigurationModule>]
	public class TestModule : ConfigureServicesModule
	{
		/// <inheritdoc />
		public override void ConfigureServices(IServiceConfigurationContext context)
		{
			context.Services.AddBlazorAssembliesContributor<TestBlazorAssembliesContributor>();
		}
	}
}
