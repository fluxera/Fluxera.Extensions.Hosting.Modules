namespace Fluxera.Extensions.Hosting.Modules.Messaging.TransactionalOutbox
{
	using JetBrains.Annotations;

	/// <summary>
	///     The database system to use for providing lock statements.
	/// </summary>
	[PublicAPI]
	public enum LockStatementProvider
	{
		/// <summary>
		///     MS SQL Server
		/// </summary>
		SqlServer,

		/// <summary>
		///     MySQL
		/// </summary>
		MySql,

		/// <summary>
		///     Postgres
		/// </summary>
		Postgres
	}
}
