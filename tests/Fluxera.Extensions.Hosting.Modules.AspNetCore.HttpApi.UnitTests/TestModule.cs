namespace Fluxera.Extensions.Hosting.Modules.AspNetCore.HttpApi.UnitTests
{
	using Fluxera.Extensions.Hosting.Modules.AspNetCore.HttpApi.OData;

	[DependsOn(typeof(ODataModule))]
	[DependsOn(typeof(HttpApiModule))]
	public class TestModule : IModule
	{
	}
}
