namespace Fluxera.Extensions.Hosting.Modules.AspNetCore.HealthChecks
{
	using Fluxera.Extensions.Hosting.Modules.AspNetCore;
	using Fluxera.Extensions.Hosting.Modules.AspNetCore.HealthChecks.Contributors;
	using Fluxera.Extensions.Hosting.Modules.HealthChecks;
	using JetBrains.Annotations;

	/// <summary>
	///     A module that enabled the health checks.
	/// </summary>
	[PublicAPI]
	public sealed class AspNetCoreHealthChecksModule : ConfigureServicesModule
	{
		/// <inheritdoc />
		public override void PreConfigureServices(IServiceConfigurationContext context)
		{
			// Add the health check route contributor.
			context.Services.AddEndpointRouteContributor<EndpointRouteContributor>();

			// Add the default health check contributor.
			context.Services.AddHealthCheckContributor<HealthChecksContributor>();
		}
	}
}
