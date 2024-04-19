#define MONGO
#undef EFCORE

namespace Catalog.Infrastructure
{
	using Catalog.Domain;
	using Catalog.Domain.Products;
	using Catalog.Infrastructure.Contributors;
	using Catalog.Infrastructure.Products;
	using Fluxera.Extensions.Hosting;
	using Fluxera.Extensions.Hosting.Modules;
	using Fluxera.Extensions.Hosting.Modules.Infrastructure;
	using Fluxera.Extensions.Hosting.Modules.Messaging;
	using Fluxera.Extensions.Hosting.Modules.Persistence;
	using Fluxera.Extensions.Hosting.Modules.Persistence.MongoDB;
	using JetBrains.Annotations;
	using Microsoft.Extensions.DependencyInjection.Extensions;

	/// <summary>
	///     The infrastructure module of the component.
	/// </summary>
	[PublicAPI]
#if EFCORE
	[DependsOn<EntityFrameworkCorePersistenceModule>]
#elif MONGO
	[DependsOn<MongoPersistenceModule>]
#endif
	[DependsOn<CatalogDomainModule>]
	[DependsOn<InfrastructureModule>]
	public sealed class CatalogInfrastructureModule : ConfigureServicesModule
	{
		/// <inheritdoc />
		public override void ConfigureServices(IServiceConfigurationContext context)
		{
			// Add the repository contributor for the 'Default' repository.
			context.Services.AddRepositoryContributor<RepositoryContributor>();

			// Add the consumer contributor.
			context.Services.AddConsumersContributor<ConsumersContributor>();

			// Add repositories.
			context.Log("AddRepositories", services =>
				services.TryAddTransient<IProductRepository, ProductRepository>());
		}
	}
}
