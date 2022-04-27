namespace Fluxera.Extensions.Hosting.Modules.Messaging.InMemory
{
	using JetBrains.Annotations;

	/// <summary>
	///     The options for the in-memory messaging module.
	/// </summary>
	[PublicAPI]
	public sealed class InMemoryMessagingOptions
	{
		/// <summary>
		///     Specify the number of concurrent messages that can be consumed (separate from prefetch count).
		/// </summary>
		public int? ConcurrentMessageLimit { get; set; }
	}
}
