namespace Fluxera.Extensions.Hosting.Modules.AspNetCore.Principal.UnitTests
{
	using Fluxera.Extensions.DependencyInjection;
	using Microsoft.AspNetCore.Http;

	[DependsOn(typeof(AspNetCorePrincipalModule))]
	public class TestModule : ConfigureServicesModule
	{
		/// <inheritdoc />
		public override void ConfigureServices(IServiceConfigurationContext context)
		{
			context.Services.ReplaceSingleton<IHttpContextAccessor, TestHttpContextAccessor>();
		}
	}
}
