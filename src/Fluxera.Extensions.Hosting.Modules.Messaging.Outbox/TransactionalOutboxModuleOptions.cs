namespace Fluxera.Extensions.Hosting.Modules.Messaging.Outbox
{
	using JetBrains.Annotations;

	/// <summary>
	///     The options for the transactional outbox messaging module.
	/// </summary>
	[PublicAPI]
	public sealed class TransactionalOutboxModuleOptions
	{
		/// <summary>
		///     Initializes a new instance of the <see cref="TransactionalOutboxModuleOptions" /> type.
		/// </summary>
		public TransactionalOutboxModuleOptions()
		{
			this.Outbox = new OutboxOptions();
			this.BusOutbox = new BusOutboxOptions();
		}

		/// <summary>
		///     Gets or sets the outbox options.
		/// </summary>
		public OutboxOptions Outbox { get; set; }

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
	}
}
