namespace Ordering.Infrastructure
{
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
	using Ordering.Domain;
	using Ordering.Domain.OrderAggregate;
	using Ordering.Infrastructure.Contexts;
	using Ordering.Infrastructure.Contributors;
	using Ordering.Infrastructure.OrderAggregate;

	/// <summary>
	///     The infrastructure module of the component.
	/// </summary>
	[PublicAPI]
	[DependsOn<OrderingDomainModule>]
	[DependsOn<RabbitMqMessagingModule>]
	[DependsOn<TransactionalOutboxModule<OrderingDbContext>>]
	[DependsOn<EntityFrameworkCorePersistenceModule>]
	[DependsOn<ConfigurationModule>]
	public sealed class OrderingInfrastructureModule : ConfigureServicesModule
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
				services.TryAddTransient<IOrderRepository, OrderRepository>());

			context.Services.AddDbContext<OrderingDbContext>();
		}
	}
}
