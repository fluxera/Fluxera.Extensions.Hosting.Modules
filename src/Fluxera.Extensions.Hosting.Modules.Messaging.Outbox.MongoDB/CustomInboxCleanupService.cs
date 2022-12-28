namespace Fluxera.Extensions.Hosting.Modules.Messaging.Outbox.MongoDB
{
	using System;
	using MassTransit;
	using MassTransit.MongoDbIntegration;
	using Microsoft.Extensions.Logging;
	using Microsoft.Extensions.Options;

	internal sealed class CustomInboxCleanupService : InboxCleanupService
	{
		/// <inheritdoc />
		public CustomInboxCleanupService(
			IOptions<InboxCleanupServiceOptions> options,
			ILogger<InboxCleanupService> logger,
			IServiceProvider serviceProvider) : base(options, logger, serviceProvider)
		{
		}
	}
}
