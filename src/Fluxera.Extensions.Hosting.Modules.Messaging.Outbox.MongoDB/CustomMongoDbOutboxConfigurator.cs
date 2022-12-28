namespace Fluxera.Extensions.Hosting.Modules.Messaging.Outbox.MongoDB
{
	using System;
	using global::MongoDB.Bson.Serialization;
	using global::MongoDB.Driver;
	using MassTransit;
	using MassTransit.Configuration;
	using MassTransit.Middleware;
	using MassTransit.MongoDbIntegration;
	using MassTransit.MongoDbIntegration.Outbox;
	using Microsoft.Extensions.DependencyInjection;
	using Microsoft.Extensions.DependencyInjection.Extensions;

	internal sealed class CustomMongoDbOutboxConfigurator : MongoDbConfigurator, IMongoDbOutboxConfigurator
	{
		private readonly IBusRegistrationConfigurator configurator;

		/// <inheritdoc />
		public CustomMongoDbOutboxConfigurator(IBusRegistrationConfigurator configurator)
		{
			this.configurator = configurator;
		}

		public TimeSpan DuplicateDetectionWindow { get; set; } = TimeSpan.FromMinutes(30.0);

		public TimeSpan QueryDelay { get; set; } = TimeSpan.FromSeconds(10.0);

		public int QueryMessageLimit { get; set; } = 100;

		public TimeSpan QueryTimeout { get; set; } = TimeSpan.FromSeconds(30.0);

		public void DisableInboxCleanupService()
		{
			this.configurator.RemoveHostedService<CustomInboxCleanupService>();
		}

		/// <inheritdoc />
		public void UseBusOutbox(Action<IMongoDbBusOutboxConfigurator> configure = null)
		{
			CustomMongoDbBusOutboxConfigurator busOutboxConfigurator = new CustomMongoDbBusOutboxConfigurator(this.configurator, this);

			busOutboxConfigurator.Configure(configure);
		}

		public void Configure(Action<IMongoDbOutboxConfigurator> configure)
		{
			configure?.Invoke(this);

			if(this.ProviderClientFactory == null)
			{
				throw new ConfigurationException("ClientFactory must be specified");
			}

			if(this.ProviderDatabaseFactory == null)
			{
				throw new ConfigurationException("DatabaseFactory must be specified");
			}

			if(this.CollectionNameFormatterFactory == null)
			{
				throw new ConfigurationException("CollectionNameFormatterFactory must be specified");
			}

			this.configurator.TryAddScoped<IOutboxContextFactory<MongoDbContext>, MongoDbOutboxContextFactory>();

			this.configurator.AddHostedService<CustomInboxCleanupService>();
			this.configurator
				.AddOptions<InboxCleanupServiceOptions>()
				.Configure(options =>
				{
					options.DuplicateDetectionWindow = this.DuplicateDetectionWindow;
					options.QueryMessageLimit = this.QueryMessageLimit;
					options.QueryDelay = this.QueryDelay;
					options.QueryTimeout = this.QueryTimeout;
				});

			RegisterClassMaps();

			this.RegisterCollectionFactory<InboxState>();
			this.RegisterCollectionFactory<OutboxMessage>();
			this.RegisterCollectionFactory<OutboxState>();

			this.configurator.TryAddScoped(this.ProviderClientFactory);
			this.configurator.TryAddScoped(this.ProviderDatabaseFactory);
			this.configurator.TryAddSingleton(this.CollectionNameFormatterFactory);

			this.configurator.TryAddScoped<MongoDbContext, CustomTransactionMongoDbContext>();
		}

		private void RegisterCollectionFactory<T>() where T : class
		{
			IMongoCollection<T> CollectionFactory(IServiceProvider serviceProvider)
			{
				IMongoDatabase database = serviceProvider.GetRequiredService<IMongoDatabase>();

				ICollectionNameFormatter collectionNameFormatter = this.CollectionNameFormatterFactory(serviceProvider);

				return database.GetCollection<T>(collectionNameFormatter.Collection<T>());
			}

			this.configurator.TryAddScoped(CollectionFactory);
			this.configurator.TryAddScoped(serviceProvider => serviceProvider.GetRequiredService<MongoDbContext>().GetCollection<T>());
		}

		private static void RegisterClassMaps()
		{
			if(!BsonClassMap.IsClassMapRegistered(typeof(InboxState)))
			{
				BsonClassMap.RegisterClassMap(new BsonClassMap<InboxState>(cfg =>
				{
					cfg.AutoMap();
					cfg.MapIdProperty(x => x.Id);
				}));
			}

			if(!BsonClassMap.IsClassMapRegistered(typeof(OutboxState)))
			{
				BsonClassMap.RegisterClassMap(new BsonClassMap<OutboxState>(cfg =>
				{
					cfg.AutoMap();
					cfg.MapIdProperty(x => x.OutboxId);
				}));
			}

			if(!BsonClassMap.IsClassMapRegistered(typeof(OutboxMessage)))
			{
				BsonClassMap.RegisterClassMap(new BsonClassMap<OutboxMessage>(cfg =>
				{
					cfg.AutoMap();
					cfg.MapIdProperty(x => x.Id);
				}));
			}
		}
	}
}
