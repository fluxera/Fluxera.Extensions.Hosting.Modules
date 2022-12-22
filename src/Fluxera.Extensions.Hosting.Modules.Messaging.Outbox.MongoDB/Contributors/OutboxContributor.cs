namespace Fluxera.Extensions.Hosting.Modules.Messaging.TransactionalOutbox.MongoDB.Contributors
{
	using Fluxera.Extensions.Hosting.Modules.Configuration;
	using global::MongoDB.Driver;
	using JetBrains.Annotations;
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
			TransactionalOutboxModuleOptions options = context.Services.GetOptions<TransactionalOutboxModuleOptions>();

			configurator.AddMongoDbOutbox(cfg =>
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
				cfg.ClientFactory(serviceProvider =>
				{
					return serviceProvider.GetRequiredService<IMongoClient>();
				});
				cfg.DatabaseFactory(serviceProvider =>
				{
					return serviceProvider.GetRequiredService<IMongoDatabase>();
				});
			});

			context.Logger.LogMongoDbInboxOutboxUsed();
		}
	}
}
