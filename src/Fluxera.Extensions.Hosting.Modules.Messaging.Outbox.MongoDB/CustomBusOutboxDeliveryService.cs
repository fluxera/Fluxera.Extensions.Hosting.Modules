namespace Fluxera.Extensions.Hosting.Modules.Messaging.Outbox.MongoDB
{
	using System;
	using MassTransit;
	using MassTransit.MongoDbIntegration;
	using Microsoft.Extensions.Logging;
	using Microsoft.Extensions.Options;

	internal sealed class CustomBusOutboxDeliveryService : BusOutboxDeliveryService
	{
		/// <inheritdoc />
		public CustomBusOutboxDeliveryService(
			IBusControl busControl,
			IOptions<OutboxDeliveryServiceOptions> options,
			ILogger<BusOutboxDeliveryService> logger,
			IServiceProvider serviceProvider) : base(busControl, options, logger, serviceProvider)
		{
		}
	}
}
