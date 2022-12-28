namespace Fluxera.Extensions.Hosting.Modules.Messaging.Outbox.EntityFrameworkCore
{
	using System;
	using MassTransit;
	using MassTransit.DependencyInjection;
	using MassTransit.EntityFrameworkCoreIntegration;
	using Microsoft.EntityFrameworkCore;
	using Microsoft.Extensions.DependencyInjection;

	internal sealed class CustomEntityFrameworkBusOutboxConfigurator<TContext> : IEntityFrameworkBusOutboxConfigurator
		where TContext : DbContext
	{
		private readonly IBusRegistrationConfigurator configurator;
		private readonly CustomEntityFrameworkOutboxConfigurator<TContext> outboxConfigurator;

		public CustomEntityFrameworkBusOutboxConfigurator(IBusRegistrationConfigurator configurator,
			CustomEntityFrameworkOutboxConfigurator<TContext> outboxConfigurator)
		{
			this.outboxConfigurator = outboxConfigurator;
			this.configurator = configurator;
		}

		/// <inheritdoc />
		public void DisableDeliveryService()
		{
			this.configurator.RemoveHostedService<BusOutboxDeliveryService<TContext>>();
		}

		/// <summary>
		///     The number of message to deliver at a time from the outbox
		/// </summary>
		public int MessageDeliveryLimit { get; set; } = 100;

		/// <summary>
		///     Transport Send timeout when delivering messages to the transport
		/// </summary>
		public TimeSpan MessageDeliveryTimeout { get; set; } = TimeSpan.FromSeconds(10);

		public void Configure(Action<IEntityFrameworkBusOutboxConfigurator> configure)
		{
			this.configurator.AddHostedService<BusOutboxDeliveryService<TContext>>();
			this.configurator.ReplaceScoped<IScopedBusContextProvider<IBus>, EntityFrameworkScopedBusContextProvider<IBus, TContext>>();

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
