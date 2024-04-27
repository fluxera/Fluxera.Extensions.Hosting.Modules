namespace Ordering.HttpApi
{
	using Fluxera.Extensions.Hosting;
	using Fluxera.Extensions.Hosting.Modules;
	using Fluxera.Extensions.Hosting.Modules.AspNetCore.HttpApi;
	using JetBrains.Annotations;
	using MadEyeMatt.AspNetCore.Endpoints;
	using Microsoft.Extensions.DependencyInjection;

	[PublicAPI]
	[DependsOn<HttpApiModule>]
	public sealed class OrderingHttpApiModule : ConfigureApplicationModule
	{
		/// <inheritdoc />
		public override void ConfigureServices(IServiceConfigurationContext context)
		{
			context.Services.Configure<EndpointsOptions>(options =>
			{
				options.EndpointsRoutePrefix = "endpoints";
			});
		}

		/// <inheritdoc />
		public override void PreConfigure(IApplicationInitializationContext context)
		{
			context.UseProblemDetails();
		}
	}
}
