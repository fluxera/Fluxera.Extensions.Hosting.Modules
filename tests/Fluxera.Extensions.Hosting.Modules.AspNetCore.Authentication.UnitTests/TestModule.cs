namespace Fluxera.Extensions.Hosting.Modules.AspNetCore.Authentication.UnitTests
{
	using Fluxera.Extensions.Hosting.Modules.AspNetCore.Authentication.ApiKey;
	using Fluxera.Extensions.Hosting.Modules.AspNetCore.Authentication.Basic;
	using Fluxera.Extensions.Hosting.Modules.AspNetCore.Authentication.Cookies;
	using Fluxera.Extensions.Hosting.Modules.AspNetCore.Authentication.JwtBearer;

	[DependsOn(typeof(ApiKeyAuthenticationModule))]
	[DependsOn(typeof(BasicAuthenticationModule))]
	[DependsOn(typeof(CookiesAuthenticationModule))]
	[DependsOn(typeof(JwtBearerAuthenticationModule))]
	[DependsOn(typeof(AuthenticationModule))]
	public class TestModule : IModule
	{
	}
}
