namespace Fluxera.Extensions.Hosting.Modules.Messaging.Scheduler
{
	using JetBrains.Annotations;

	/// <summary>
	///     The options for the scheduler messaging module.
	/// </summary>
	[PublicAPI]
	public sealed class SchedulerMessagingOptions
	{
		/// <summary>
		///		Gets or sets the prefetch count.
		/// </summary>
		public int? PrefetchCount { get; set; } = 32;

		/// <summary>
		///		Gets or sets the concurrent message limit.
		/// </summary>
		public int? ConcurrentMessageLimit { get; set; }
	}
}
