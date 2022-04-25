namespace Fluxera.Extensions.Hosting.Modules.AspNetCore.HttpApi.UnitTests
{
	using Fluxera.Extensions.Hosting.Modules.AspNetCore.HttpApi.OData;
	using Fluxera.Extensions.Hosting.Modules.AspNetCore.HttpApi.OData.Versioning;
	using Fluxera.Extensions.Hosting.Modules.AspNetCore.HttpApi.Swagger;
	using Fluxera.Extensions.Hosting.Modules.AspNetCore.HttpApi.Versioning;

	[DependsOn(typeof(SwaggerModule))]
	[DependsOn(typeof(ODataVersioningModule))]
	[DependsOn(typeof(VersioningModule))]
	[DependsOn(typeof(ODataModule))]
	[DependsOn(typeof(HttpApiModule))]
	public class TestModule : IModule
	{
	}
}
