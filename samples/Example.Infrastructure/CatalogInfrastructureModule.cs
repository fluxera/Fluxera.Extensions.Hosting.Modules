namespace Catalog.Infrastructure
{
	using Catalog.Domain;
	using Catalog.Domain.Product;
	using Catalog.Infrastructure.Contexts;
	using Catalog.Infrastructure.Contributors;
	using Catalog.Infrastructure.Product;
	using Fluxera.Extensions.Hosting;
	using Fluxera.Extensions.Hosting.Modules;
	using Fluxera.Extensions.Hosting.Modules.Configuration;
	using Fluxera.Extensions.Hosting.Modules.Messaging.RabbitMQ;
	using Fluxera.Extensions.Hosting.Modules.Messaging.TransactionalOutbox;
	using Fluxera.Extensions.Hosting.Modules.Persistence;
	using Fluxera.Extensions.Hosting.Modules.Persistence.EntityFrameworkCore;
	using JetBrains.Annotations;
	using Microsoft.Extensions.DependencyInjection;
	using Microsoft.Extensions.DependencyInjection.Extensions;

	/// <summary>
	///     The infrastructure module of the component.
	/// </summary>
	[PublicAPI]
	[DependsOn<CatalogDomainModule>]
	[DependsOn<RabbitMqMessagingModule>]
	[DependsOn<TransactionalOutboxModule<CatalogDbContext>>]
	[DependsOn<EntityFrameworkCorePersistenceModule>]
	[DependsOn<ConfigurationModule>]
	public sealed class CatalogInfrastructureModule : ConfigureServicesModule
	{
		/// <inheritdoc />
		public override void ConfigureServices(IServiceConfigurationContext context)
		{
			// Add the repository contributor for the 'Default' repository.
			context.Services.AddRepositoryContributor<RepositoryContributor>("Default");

			// Add the repository context contributor for the 'Default' repository.
			context.Services.AddRepositoryContextContributor<RepositoryContextContributor>("Default");

			// Add repositories.
			context.Log("AddRepositories", services =>
				services.TryAddTransient<IProductRepository, ProductRepository>());

			context.Services.AddDbContext<CatalogDbContext>();
		}
	}
}
