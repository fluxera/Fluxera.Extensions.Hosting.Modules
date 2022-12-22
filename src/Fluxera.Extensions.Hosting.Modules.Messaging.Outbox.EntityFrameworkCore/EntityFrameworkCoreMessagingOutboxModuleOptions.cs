namespace Fluxera.Extensions.Hosting.Modules.Messaging.Outbox.EntityFrameworkCore
{
	using JetBrains.Annotations;

	/// <summary>
	///     The options for the transactional outbox messaging module.
	/// </summary>
	[PublicAPI]
	public sealed class EntityFrameworkCoreMessagingOutboxModuleOptions
	{
		/// <summary>
		///     Initializes a new instance of the <see cref="EntityFrameworkCoreMessagingOutboxModuleOptions" /> type.
		/// </summary>
		public EntityFrameworkCoreMessagingOutboxModuleOptions()
		{
			this.Outbox = new EntityFrameworkCoreOutboxOptions();
			this.BusOutbox = new BusOutboxOptions();
		}

		/// <summary>
		///     Gets or sets the outbox options.
		/// </summary>
		public EntityFrameworkCoreOutboxOptions Outbox { get; set; }

		/// <summary>
		///     Gets or sets the bus outbox options.
		/// </summary>
		public BusOutboxOptions BusOutbox { get; set; }

		/// <summary>
		///     Flag, indicating if the cleanup background service is enabled.
		/// </summary>
		public bool InboxCleanupServiceEnabled { get; set; } = true;

		/// <summary>
		///     Flag, indicating if the delivery background service is enabled.
		/// </summary>
		public bool DeliveryServiceEnabled { get; set; } = true;

		/// <summary>
		///		Gets or sets the name of the repository to use for the outbox.
		/// </summary>
		public string RepositoryName { get; set; } = "Default";
	}
}
