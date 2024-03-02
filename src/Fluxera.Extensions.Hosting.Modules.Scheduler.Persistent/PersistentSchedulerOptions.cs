namespace Fluxera.Extensions.Hosting.Modules.Scheduler.Persistent
{
	using System;
	using JetBrains.Annotations;

	/// <summary>
	///     The options for the persistent Quartz store module.
	/// </summary>
	[PublicAPI]
	public sealed class PersistentSchedulerOptions
	{
		/// <summary>
		///		Gets or sets the database to use.
		/// </summary>
		public DatabaseKind Database { get; set; } = DatabaseKind.SQLite;

		/// <summary>
		///		Gets or sets the name of the connection string to use.
		/// </summary>
		public string ConnectionStringName { get; set; } = "Scheduler";

		/// <summary>
		///		Set whether database schema validated will be tried during scheduler initialization.
		/// </summary>
		/// <remarks>
		///		Optional feature and all providers do not support it.
		/// </remarks>
		public bool PerformSchemaValidation { get; set; }

		/// <summary>
		///		Sets the database retry interval.
		/// </summary>
		/// <remarks>
		///		Defaults to 15 seconds.
		/// </remarks>
		public TimeSpan RetryInterval { get; set; } = TimeSpan.FromSeconds(15);

		/// <summary>
		///		Set whether string-only properties will be handled in JobDataMaps.
		/// </summary>
		public bool UseProperties { get; set; }

		/// <summary>
		///		The time span by which a check-in must have missed its
		///		next-fire-time, in order for it to be considered "misfired" and thus
		///		other scheduler instances in a cluster can consider a "misfired" scheduler
		///		instance as failed or dead.
		/// </summary>
		/// <remarks>
		///		Defaults to 7500 milliseconds.
		/// </remarks>
		public TimeSpan CheckinMisfireThreshold { get; set; } = TimeSpan.FromMilliseconds(7_500);

		/// <summary>
		///		Sets the frequency at which this instance "checks-in"
		///		with the other instances of the cluster. -- Affects the rate of
		///		detecting failed instances.
		/// </summary>
		/// <remarks>
		///		Defaults to 7500 milliseconds.
		/// </remarks>
		public TimeSpan CheckinInterval { get; set; } = TimeSpan.FromMilliseconds(7_500);
	}
}
