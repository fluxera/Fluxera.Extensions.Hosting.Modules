namespace Fluxera.Extensions.Hosting.Modules.AspNetCore.HttpApi.UnitTests
{
	using Fluxera.Extensions.Hosting.Modules.Configuration;

	[DependsOn<HttpApiModule>]
	[DependsOn<ConfigurationModule>]
	public class TestModule : IModule
	{
	}
}
