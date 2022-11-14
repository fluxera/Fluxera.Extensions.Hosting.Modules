namespace Fluxera.Extensions.Hosting.Modules.Persistence
{
	using System;
	using JetBrains.Annotations;

	/// <summary>
	///     The options of a repository.
	/// </summary>
	[PublicAPI]
	public sealed class RepositoryOptions
	{
		/// <summary>
		///     Creates a new instance of the <see cref="RepositoryOptions" /> type.
		/// </summary>
		public RepositoryOptions()
		{
			this.ConnectionStringName = "Database";
		}

		/// <summary>
		///     Gets the name of the repository provider.
		/// </summary>
		public string ProviderName { get; set; }

		/// <summary>
		///     Gets the name of the database.
		/// </summary>
		public string DatabaseName { get; set; }

		/// <summary>
		///     Gets the database name prefix.
		/// </summary>
		public string DatabaseNamePrefix { get; set; }

		/// <summary>
		///     Gets the name of the connection string to use.
		/// </summary>
		public string ConnectionStringName { get; set; }

		/// <summary>
		///     Flag, indicating if the repository uses UoW. The default is <c>true</c>.
		/// </summary>
		public bool EnableUnitOfWork { get; set; } = true;

		/// <summary>
		///     Gets or sets the repository context type to use for this repository.
		/// </summary>
		public Type ContextType { get; set; }
	}
}
