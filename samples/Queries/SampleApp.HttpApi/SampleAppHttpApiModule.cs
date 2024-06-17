namespace SampleApp.HttpApi
{
	using Fluxera.Extensions.Hosting;
	using Fluxera.Extensions.Hosting.Modules;
	using Fluxera.Extensions.Hosting.Modules.AspNetCore.HttpApi;
	using Fluxera.Queries.AspNetCore;
	using JetBrains.Annotations;
	using MadEyeMatt.AspNetCore.Endpoints;
	using Microsoft.Extensions.DependencyInjection;

	/// <summary>
	///		The module for the HTTP API.
	/// </summary>
	[PublicAPI]
	[DependsOn<HttpApiModule>]
	public sealed class SampleAppHttpApiModule : ConfigureApplicationModule
	{
		/// <inheritdoc />
		public override void ConfigureServices(IServiceConfigurationContext context)
		{
			context.Services.Configure<EndpointsOptions>(options =>
			{
				options.EndpointsRoutePrefix = "api";
			});

			context.Services.AddDataQueriesSwagger();
		}

		/// <inheritdoc />
		public override void PreConfigure(IApplicationInitializationContext context)
		{
			context.UseProblemDetails();
		}
	}
}
