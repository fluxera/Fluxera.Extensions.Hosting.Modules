namespace Fluxera.Extensions.Hosting.Modules.Messaging.Outbox.InMemory
{
	using Fluxera.Extensions.Hosting.Modules.Messaging.Outbox.InMemory.Contributors;
	using JetBrains.Annotations;

	/// <summary>
	///     A module that enables the in-memory transactional outbox.
	/// </summary>
	[PublicAPI]
	[DependsOn<MessagingOutboxModule>]
	public sealed class InMemoryMessagingOutboxModule : ConfigureServicesModule
	{
		/// <inheritdoc />
		public override void ConfigureServices(IServiceConfigurationContext context)
		{
			// Add the outbox contributor.
			context.Services.AddOutboxContributor<OutboxContributor>();
		}
	}
}
