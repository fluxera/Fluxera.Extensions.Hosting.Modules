namespace Fluxera.Extensions.Hosting.Modules.Persistence.EntityFrameworkCore
{
	using Fluxera.Extensions.Hosting.Modules.Configuration;
	using Fluxera.Extensions.Hosting.Modules.HealthChecks;
	using Fluxera.Extensions.Hosting.Modules.OpenTelemetry;
	using Fluxera.Extensions.Hosting.Modules.Persistence.EntityFrameworkCore.Contributors;
	using JetBrains.Annotations;

	/// <summary>
	///     A module that enables EntityFramework Core persistence.
	/// </summary>
	[PublicAPI]
	[DependsOn<HealthChecksModule>]
	[DependsOn<PersistenceModule>]
	public sealed class EntityFrameworkCorePersistenceModule : ConfigureServicesModule
	{
		/// <inheritdoc />
		public override void PreConfigureServices(IServiceConfigurationContext context)
		{
			// Add the configure options contributor.
			context.Services.AddConfigureOptionsContributor<ConfigureOptionsContributor>();

			// Add the tracer provider contributor.
			context.Services.AddTracerProviderContributor<TracerProviderContributor>();

			// Add the health checks contributor.
			context.Services.AddHealthCheckContributor<HealthChecksContributor>();

			// Add the repository provider contributor.
			context.Log("AddRepositoryProviderContributor(EntityFrameworkCore)",
				services => services.AddRepositoryProviderContributor<RepositoryProviderContributor>());
		}
	}
}
