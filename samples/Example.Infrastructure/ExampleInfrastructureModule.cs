namespace Catalog.Infrastructure
{
	using Catalog.Domain;
	using Catalog.Domain.Example;
	using Catalog.Infrastructure.Contributors;
	using Catalog.Infrastructure.Example;
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
	[DependsOn<ExampleDomainModule>]
	[DependsOn<RabbitMqMessagingModule>]
	[DependsOn<TransactionalOutboxModule<ExampleDbContext>>]
	[DependsOn<EntityFrameworkCorePersistenceModule>]
	[DependsOn<ConfigurationModule>]
	public sealed class ExampleInfrastructureModule : ConfigureServicesModule
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
				services.TryAddTransient<IExampleRepository, ExampleRepository>());

			context.Services.AddDbContext<ExampleDbContext>();
		}
	}
}
