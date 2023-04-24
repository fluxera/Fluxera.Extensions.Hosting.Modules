#undef EFCORE
#define MONGO

namespace Catalog.Infrastructure
{
	using Catalog.Domain;
	using Catalog.Infrastructure.Contributors;
	using Fluxera.Extensions.Hosting;
	using Fluxera.Extensions.Hosting.Modules;
	using Fluxera.Extensions.Hosting.Modules.Configuration;
	using Fluxera.Extensions.Hosting.Modules.Messaging.RabbitMQ;
	using Fluxera.Extensions.Hosting.Modules.Persistence;
	using Fluxera.Extensions.Hosting.Modules.Persistence.MongoDB;
	using JetBrains.Annotations;

	/// <summary>
	///     The infrastructure module of the component.
	/// </summary>
	[PublicAPI]
	[DependsOn<CatalogDomainModule>]
	[DependsOn<RabbitMqMessagingModule>]
#if EFCORE
	//[DependsOn<EntityFrameworkCoreMessagingOutboxModule>]
	[DependsOn<EntityFrameworkCorePersistenceModule>]
#elif MONGO
	//[DependsOn<MongoMessagingOutboxModule>]
	[DependsOn<MongoPersistenceModule>]
#endif
	[DependsOn<ConfigurationModule>]
	public sealed class CatalogInfrastructureModule : ConfigureServicesModule
	{
		/// <inheritdoc />
		public override void ConfigureServices(IServiceConfigurationContext context)
		{
			// Add the repository contributor for the 'Default' repository.
			context.Services.AddRepositoryContributor<RepositoryContributor>();
		}
	}
}
