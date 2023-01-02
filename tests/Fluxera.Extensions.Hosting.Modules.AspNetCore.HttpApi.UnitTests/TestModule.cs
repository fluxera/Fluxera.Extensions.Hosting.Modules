namespace Fluxera.Extensions.Hosting.Modules.AspNetCore.HttpApi.UnitTests
{
	using Fluxera.Extensions.Hosting.Modules.AspNetCore.HttpApi.OData;
	using Fluxera.Extensions.Hosting.Modules.Configuration;

	[DependsOn<HttpApiModule>]
	[DependsOn<ODataModule>]
	[DependsOn<ConfigurationModule>]
	public class TestModule : IModule
	{
	}
}
