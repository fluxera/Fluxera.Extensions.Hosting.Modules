namespace Fluxera.Extensions.Hosting.Modules.Messaging.Outbox
{
	using System;
	using JetBrains.Annotations;

	/// <summary>
	///     The options for the outbox.
	/// </summary>
	[PublicAPI]
	public class OutboxOptions
	{
		/// <summary>
		///     The amount of time a message remains in the inbox for duplicate detection (based on MessageId).
		/// </summary>
		public TimeSpan? DuplicateDetectionWindow { get; set; }

		/// <summary>
		///     The maximum number of messages to query from the database at a time.
		/// </summary>
		public int? QueryMessageLimit { get; set; }

		/// <summary>
		///     Database query timeout.
		/// </summary>
		public TimeSpan? QueryTimeout { get; set; }

		/// <summary>
		///     The delay between queries once messages are no longer available. When a query returns messages, subsequent queries
		///     are performed until no messages are returned after which the QueryDelay is used.
		/// </summary>
		public TimeSpan? QueryDelay { get; set; }
	}
}
