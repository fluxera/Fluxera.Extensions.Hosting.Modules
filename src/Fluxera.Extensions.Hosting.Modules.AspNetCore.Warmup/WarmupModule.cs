namespace Fluxera.Extensions.Hosting.Modules.AspNetCore.Warmup
{
	using Fluxera.Extensions.Hosting.Modules.AspNetCore.HealthChecks;
	using Fluxera.Extensions.Hosting.Modules.AspNetCore.Warmup.Contributors;
	using Fluxera.Extensions.Hosting.Modules.HttpClient;
	using JetBrains.Annotations;
	using Microsoft.Extensions.DependencyInjection;
	using Microsoft.Extensions.DependencyInjection.Extensions;

	/// <summary>
	///     A module that enabled service warmup.
	/// </summary>
	[PublicAPI]
	[DependsOn(typeof(HttpClientModule))]
	[DependsOn(typeof(HealthChecksModule))]
	[DependsOn(typeof(AspNetCoreModule))]
	public sealed class WarmupModule : ConfigureServicesModule
	{
		/// <inheritdoc />
		public override void ConfigureServices(IServiceConfigurationContext context)
		{
			// Add the service warmup services.
			context.Log("AddEndpointInit",
				services =>
				{
					services
						.AddSingleton<IEndpointInit, ServiceDependenciesEndpointInit>()
						.AddSingleton<IEndpointInit, ActionsEndpointInit>()
						.AddSingleton<IEndpointInit, ControllersEndpointInit>()
						.TryAddSingleton(context.Services);
				});


			// Add the readiness health check contributor.
			context.Services.AddHealthCheckContributor<WarmupHealthCheckContributor>();
		}
	}
}
