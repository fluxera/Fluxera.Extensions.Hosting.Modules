namespace Fluxera.Extensions.Hosting.Modules.Persistence.MongoDB
{
	using Fluxera.Extensions.Hosting.Modules.HealthChecks;
	using Fluxera.Extensions.Hosting.Modules.OpenTelemetry;
	using Fluxera.Extensions.Hosting.Modules.Persistence.MongoDB.Contributors;
	using Fluxera.Extensions.Hosting.Modules.Persistence.MongoDB.Sequence;
	using JetBrains.Annotations;
	using Microsoft.Extensions.DependencyInjection.Extensions;

	/// <summary>
	///     A module that enabled MongoDB persistence.
	/// </summary>
	[PublicAPI]
	[DependsOn<HealthChecksModule>]
	[DependsOn<PersistenceModule>]
	public sealed class MongoPersistenceModule : ConfigureServicesModule
	{
		/// <inheritdoc />
		public override void PreConfigureServices(IServiceConfigurationContext context)
		{
			// Add the tracer provider contributor.
			context.Services.AddTracerProviderContributor<TracerProviderContributor>();

			// Add the health checks contributor.
			context.Services.AddHealthCheckContributor<HealthChecksContributor>();

			// Add the repository provider contributor.
			context.Log("AddRepositoryProviderContributor(MongoDB)",
				services => services.AddRepositoryProviderContributor<RepositoryProviderContributor>());
		}

		/// <inheritdoc />
		public override void ConfigureServices(IServiceConfigurationContext context)
		{
			context.Log("AddSequenceServiceFactory",
				services => services.TryAddTransient<ISequenceServiceFactory, SequenceServiceFactory>());
		}
	}
}
