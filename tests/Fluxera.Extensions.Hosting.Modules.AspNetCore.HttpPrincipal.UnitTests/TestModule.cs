namespace Fluxera.Extensions.Hosting.Modules.AspNetCore.HttpPrincipal.UnitTests
{
	using Fluxera.Extensions.DependencyInjection;
	using Microsoft.AspNetCore.Http;

	[DependsOn(typeof(HttpPrincipalModule))]
	public class TestModule : ConfigureServicesModule
	{
		/// <inheritdoc />
		public override void ConfigureServices(IServiceConfigurationContext context)
		{
			context.Services.ReplaceSingleton<IHttpContextAccessor, TestHttpContextAccessor>();
		}
	}
}
