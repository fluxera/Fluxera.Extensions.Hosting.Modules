namespace Fluxera.Extensions.Hosting.Modules.Scheduler.Persistent
{
	using JetBrains.Annotations;

	/// <summary>
	///		The supported databases.
	/// </summary>
	[PublicAPI]
	public enum DatabaseKind
	{
		/// <summary>
		///		Microsoft SQL database server.
		/// </summary>
		SQLServer,

		/// <summary>
		///		MySQL database server.
		/// </summary>
		MySQL,

		/// <summary>
		///		SQLite database.
		/// </summary>
		SQLite
	}
}
