namespace Fluxera.Extensions.Hosting.Modules.Messaging.Outbox
{
	using System;
	using JetBrains.Annotations;

	/// <summary>
	///     The options for the bus outbox.
	/// </summary>
	[PublicAPI]
	public sealed class BusOutboxOptions
	{
		/// <summary>
		///     Flag, indicating if the bus outbox is activated.
		/// </summary>
		public bool UseBusOutbox { get; set; } = true;

		/// <summary>
		///     The number of messages to deliver at a time from the outbox to the broker
		/// </summary>
		public int? MessageDeliveryLimit { get; set; }

		/// <summary>
		///     Transport Send timeout when delivering messages to the transport
		/// </summary>
		public TimeSpan? MessageDeliveryTimeout { get; set; }
	}
}
