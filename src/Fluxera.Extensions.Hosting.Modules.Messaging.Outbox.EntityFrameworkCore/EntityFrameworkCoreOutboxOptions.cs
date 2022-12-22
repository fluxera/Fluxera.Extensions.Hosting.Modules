namespace Fluxera.Extensions.Hosting.Modules.Messaging.TransactionalOutbox.EntityFrameworkCore
{
	using System.Data;
	using JetBrains.Annotations;

	/// <summary>
	///     The options for the outbox.
	/// </summary>
	[PublicAPI]
	public sealed class EntityFrameworkCoreOutboxOptions : OutboxOptions
	{
		/// <summary>
		///     Gets or sets the lock statement provider to use.
		/// </summary>
		public LockStatementProvider LockStatementProvider { get; set; } = LockStatementProvider.SqlServer;

		/// <summary>
		///     The isolation level to use.
		/// </summary>
		public IsolationLevel? IsolationLevel { get; set; }
	}
}
