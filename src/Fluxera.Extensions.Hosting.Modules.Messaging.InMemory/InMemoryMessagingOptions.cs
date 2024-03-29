﻿namespace Fluxera.Extensions.Hosting.Modules.Messaging.InMemory
{
	using JetBrains.Annotations;

	/// <summary>
	///     The options for the in-memory messaging module.
	/// </summary>
	[PublicAPI]
	public sealed class InMemoryMessagingOptions
	{
		/// <summary>
		///     Gets or sets a value indicating whether the in-memory outbox is enabled.
		/// </summary>
		/// <value>
		///     <c>true</c> if the in-memory outbox is enabled; otherwise, <c>false</c>.
		/// </value>
		public bool InMemoryOutboxEnabled { get; set; } = true;

		/// <summary>
		///     Specify the number of concurrent messages that can be consumed (separate from prefetch count).
		/// </summary>
		public int? ConcurrentMessageLimit { get; set; }
	}
}
