namespace Fluxera.Extensions.Hosting.Modules.Messaging.Outbox.MongoDB.Contributors
{
	using Fluxera.Extensions.Hosting.Modules.Configuration;
	using Fluxera.Repository;
	using Fluxera.Repository.MongoDB;
	using JetBrains.Annotations;
	using MassTransit;
	using Microsoft.Extensions.DependencyInjection;
	using Microsoft.Extensions.Options;

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
					MessagingOutboxModuleOptions outboxOptions = serviceProvider.GetRequiredService<IOptions<MessagingOutboxModuleOptions>>().Value;
					MongoContextProvider contextProvider = serviceProvider.GetRequiredService<MongoContextProvider>();
					MongoContext mongoContext = contextProvider.GetContextFor((RepositoryName)outboxOptions.RepositoryName);

					return mongoContext.Client;
				});
				cfg.DatabaseFactory(serviceProvider =>
				{
					MessagingOutboxModuleOptions outboxOptions = serviceProvider.GetRequiredService<IOptions<MessagingOutboxModuleOptions>>().Value;
					MongoContextProvider contextProvider = serviceProvider.GetRequiredService<MongoContextProvider>();
					MongoContext mongoContext = contextProvider.GetContextFor((RepositoryName)outboxOptions.RepositoryName);

					return mongoContext.Database;
				});
			});

			context.Logger.LogMongoDbInboxOutboxUsed();
		}
	}
}
