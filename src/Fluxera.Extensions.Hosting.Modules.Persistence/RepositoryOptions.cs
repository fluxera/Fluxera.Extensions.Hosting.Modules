namespace Fluxera.Extensions.Hosting.Modules.Persistence
{
	using System.Collections.Generic;
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
			this.Settings = new Dictionary<string, string>();
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
		///     Gets the repository provider specific settings.
		/// </summary>
		public IDictionary<string, string> Settings { get; set; }
	}
}
