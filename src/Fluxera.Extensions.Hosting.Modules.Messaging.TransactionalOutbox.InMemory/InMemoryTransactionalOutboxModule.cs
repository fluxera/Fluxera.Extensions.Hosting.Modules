namespace Fluxera.Extensions.Hosting.Modules.Messaging.TransactionalOutbox.InMemory
{
	using Fluxera.Extensions.Hosting.Modules.Messaging.TransactionalOutbox.InMemory.Contributors;
	using JetBrains.Annotations;

	/// <summary>
	///     A module that enables the in-memory transactional outbox.
	/// </summary>
	[PublicAPI]
	[DependsOn<TransactionalOutboxModule>]
	public sealed class InMemoryTransactionalOutbox : ConfigureServicesModule
	{
		/// <inheritdoc />
		public override void ConfigureServices(IServiceConfigurationContext context)
		{
			// Add the outbox contributor.
			context.Services.AddOutboxContributor<OutboxContributor>();
		}
	}
}
