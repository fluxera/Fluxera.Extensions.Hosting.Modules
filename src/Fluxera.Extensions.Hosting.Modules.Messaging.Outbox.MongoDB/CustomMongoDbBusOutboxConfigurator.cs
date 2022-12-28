namespace Fluxera.Extensions.Hosting.Modules.Messaging.Outbox.MongoDB
{
	using System;
	using MassTransit;
	using MassTransit.DependencyInjection;
	using MassTransit.MongoDbIntegration;
	using Microsoft.Extensions.DependencyInjection;

	internal sealed class CustomMongoDbBusOutboxConfigurator : IMongoDbBusOutboxConfigurator
	{
		private readonly IBusRegistrationConfigurator configurator;
		private readonly CustomMongoDbOutboxConfigurator outboxConfigurator;

		public CustomMongoDbBusOutboxConfigurator(IBusRegistrationConfigurator configurator, CustomMongoDbOutboxConfigurator outboxConfigurator)
		{
			this.configurator = configurator;
			this.outboxConfigurator = outboxConfigurator;
		}

		/// <inheritdoc />
		public void DisableDeliveryService()
		{
			this.configurator.RemoveHostedService<CustomBusOutboxDeliveryService>();
		}

		/// <summary>
		///     The number of message to deliver at a time from the outbox
		/// </summary>
		public int MessageDeliveryLimit { get; set; } = 100;

		/// <summary>
		///     Transport Send timeout when delivering messages to the transport
		/// </summary>
		public TimeSpan MessageDeliveryTimeout { get; set; } = TimeSpan.FromSeconds(10.0);

		public void Configure(Action<IMongoDbBusOutboxConfigurator> configure)
		{
			this.configurator.AddHostedService<CustomBusOutboxDeliveryService>();
			this.configurator.ReplaceScoped<IScopedBusContextProvider<IBus>, MongoDbScopedBusContextProvider<IBus>>();

			this.configurator
				.AddOptions<OutboxDeliveryServiceOptions>()
				.Configure(options =>
				{
					options.QueryDelay = this.outboxConfigurator.QueryDelay;
					options.QueryMessageLimit = this.outboxConfigurator.QueryMessageLimit;
					options.QueryTimeout = this.outboxConfigurator.QueryTimeout;
					options.MessageDeliveryLimit = this.MessageDeliveryLimit;
					options.MessageDeliveryTimeout = this.MessageDeliveryTimeout;
				});

			configure?.Invoke(this);
		}
	}
}
