namespace Fluxera.Extensions.Hosting.Modules.Messaging.Outbox.MongoDB.Contributors
{
	using System;
	using Fluxera.Extensions.Hosting.Modules.Configuration;
	using Fluxera.Extensions.Hosting.Modules.Persistence.MongoDB;
	using Fluxera.Guards;
	using JetBrains.Annotations;
	using MadEyeMatt.MongoDB.DbContext;
	using MassTransit;
	using Microsoft.Extensions.DependencyInjection;

	/// <summary>
	///     A base class for in-memory transactional outbox configuration.
	/// </summary>
	[UsedImplicitly]
	internal sealed class OutboxContributor : IOutboxContributor
	{
		/// <inheritdoc />
		public void ConfigureOutbox(IBusRegistrationConfigurator configurator, IServiceConfigurationContext context)
		{
			MessagingOutboxModuleOptions options = context.Services.GetOptions<MessagingOutboxModuleOptions>();

			MongoPersistenceOptions persistenceOptions = context.Services.GetOptions<MongoPersistenceOptions>();
			MongoRepositoryOptions repositoryOptions = persistenceOptions.Repositories.GetOptionsOrDefault(options.RepositoryName);

			Type dbContextType = Type.GetType(repositoryOptions.DbContextType);
			dbContextType = Guard.Against.Null(dbContextType,
				message: $"The db context must be configured for MongoDB repository '{options.RepositoryName}' to be used with the transactional inbox/outbox.");

			configurator.AddMongoOutbox(cfg =>
			{
				if(options.Outbox.QueryDelay.HasValue)
				{
					cfg.QueryDelay = options.Outbox.QueryDelay.Value;
				}

				if(options.Outbox.QueryTimeout.HasValue)
				{
					cfg.QueryTimeout = options.Outbox.QueryTimeout.Value;
				}

				if(options.Outbox.QueryMessageLimit.HasValue)
				{
					cfg.QueryMessageLimit = options.Outbox.QueryMessageLimit.Value;
				}

				if(options.Outbox.DuplicateDetectionWindow.HasValue)
				{
					cfg.DuplicateDetectionWindow = options.Outbox.DuplicateDetectionWindow.Value;
				}

				// Disable cleanup service if requested.
				if(!options.InboxCleanupServiceEnabled)
				{
					cfg.DisableInboxCleanupService();
				}

				if(options.BusOutbox.UseBusOutbox)
				{
					cfg.UseBusOutbox(config =>
					{
						// Disable delivery service if requested.
						if(!options.DeliveryServiceEnabled)
						{
							config.DisableDeliveryService();
						}

						if(options.BusOutbox.MessageDeliveryLimit.HasValue)
						{
							config.MessageDeliveryLimit = options.BusOutbox.MessageDeliveryLimit.Value;
						}

						if(options.BusOutbox.MessageDeliveryTimeout.HasValue)
						{
							config.MessageDeliveryTimeout = options.BusOutbox.MessageDeliveryTimeout.Value;
						}
					});
				}

				// MongoDB specific configuration.
				cfg.CollectionNameFormatter(_ => new PluralizeCollectionNameFormatter());
				cfg.ClientFactory(serviceProvider =>
				{
					using(IServiceScope scope = serviceProvider.CreateScope())
					{
						MongoDbContext dbContext = (MongoDbContext)scope.ServiceProvider.GetRequiredService(dbContextType);
						return dbContext.Client;
					}
				});
				cfg.DatabaseFactory(serviceProvider =>
				{
					using(IServiceScope scope = serviceProvider.CreateScope())
					{
						MongoDbContext dbContext = (MongoDbContext)scope.ServiceProvider.GetRequiredService(dbContextType);
						return dbContext.Database;
					}
				});
			});

			context.Logger.LogMongoDbInboxOutboxUsed();
		}
	}
}
