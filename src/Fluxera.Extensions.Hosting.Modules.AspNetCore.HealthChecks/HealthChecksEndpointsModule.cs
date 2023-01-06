namespace Fluxera.Extensions.Hosting.Modules.AspNetCore.HealthChecks
{
	using Fluxera.Extensions.Hosting.Modules.AspNetCore.HealthChecks.Contributors;
	using Fluxera.Extensions.Hosting.Modules.Configuration;
	using Fluxera.Extensions.Hosting.Modules.HealthChecks;
	using JetBrains.Annotations;

	/// <summary>
	///     A module that enabled the health checks.
	/// </summary>
	[PublicAPI]
	[DependsOn<AspNetCoreModule>]
	[DependsOn<HealthChecksModule>]
	[DependsOn<ConfigurationModule>]
	public sealed class HealthChecksEndpointsModule : ConfigureServicesModule
	{
		/// <inheritdoc />
		public override void PreConfigureServices(IServiceConfigurationContext context)
		{
			// Add the configure options contributor.
			context.Services.AddConfigureOptionsContributor<ConfigureOptionsContributor>();

			// Add the health check route contributor.
			context.Services.AddEndpointRouteContributor<EndpointRouteContributor>();

			// Add the health checks contributor.
			context.Services.AddHealthCheckContributor<HealthChecksContributor>();
		}
	}
}
