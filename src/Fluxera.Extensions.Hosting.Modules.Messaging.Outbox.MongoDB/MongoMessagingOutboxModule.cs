namespace Fluxera.Extensions.Hosting.Modules.Messaging.Outbox.MongoDB
{
	using Fluxera.Extensions.Hosting.Modules.Messaging.Outbox.MongoDB.Contributors;
	using JetBrains.Annotations;

	/// <summary>
	///     A module that enables the MongoDB transactional outbox.
	/// </summary>
	[PublicAPI]
	[DependsOn<MessagingOutboxModule>]
	public sealed class MongoMessagingOutboxModule : ConfigureServicesModule
	{
		/// <inheritdoc />
		public override void ConfigureServices(IServiceConfigurationContext context)
		{
			// Add the outbox contributor.
			context.Services.AddOutboxContributor<OutboxContributor>();
		}
	}
}
