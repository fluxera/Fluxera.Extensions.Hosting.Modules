namespace Fluxera.Extensions.Hosting.Modules.Messaging.Outbox.MongoDB
{
	using System;
	using MassTransit;
	using MassTransit.Middleware.Outbox;
	using MassTransit.MongoDbIntegration;
	using Microsoft.Extensions.Logging;
	using Microsoft.Extensions.Options;

	internal sealed class CustomBusOutboxDeliveryService : BusOutboxDeliveryService
	{
		/// <inheritdoc />
		public CustomBusOutboxDeliveryService(
			IBusControl busControl,
			IOptions<OutboxDeliveryServiceOptions> options,
			IBusOutboxNotification notification,
			ILogger<BusOutboxDeliveryService> logger,
			IServiceProvider serviceProvider) : base(busControl, options, notification, logger, serviceProvider)
		{
		}
	}
}
