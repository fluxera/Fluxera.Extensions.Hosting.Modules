﻿namespace Ordering.Infrastructure
{
	using Fluxera.Extensions.Hosting;
	using Fluxera.Extensions.Hosting.Modules;
	using Fluxera.Extensions.Hosting.Modules.Configuration;
	using Fluxera.Extensions.Hosting.Modules.Messaging.RabbitMQ;
	using Fluxera.Extensions.Hosting.Modules.Messaging.TransactionalOutbox.EntityFrameworkCore;
	using Fluxera.Extensions.Hosting.Modules.Persistence;
	using Fluxera.Extensions.Hosting.Modules.Persistence.EntityFrameworkCore;
	using JetBrains.Annotations;
	using Ordering.Domain;
	using Ordering.Infrastructure.Contexts;
	using Ordering.Infrastructure.Contributors;

	/// <summary>
	///     The infrastructure module of the component.
	/// </summary>
	[PublicAPI]
	[DependsOn<OrderingDomainModule>]
	[DependsOn<RabbitMqMessagingModule>]
	[DependsOn<EntityFrameworkCoreTransactionalOutboxModule<OrderingDbContext>>]
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

			// Add the db context.
			context.Services.AddDbContext(typeof(OrderingDbContext));
		}
	}
}