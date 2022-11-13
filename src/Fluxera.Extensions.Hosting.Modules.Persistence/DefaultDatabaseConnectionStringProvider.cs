namespace Fluxera.Extensions.Hosting.Modules.Persistence
{
	using Fluxera.Repository;
	using JetBrains.Annotations;
	using Microsoft.Extensions.Options;

	/// <summary>
	///     A default database connection string provider that gets the name from configuration.
	/// </summary>
	[PublicAPI]
	public class DefaultDatabaseConnectionStringProvider : IDatabaseConnectionStringProvider
	{
		/// <summary>
		///     Crates a new instance of the <see cref="DefaultDatabaseNameProvider" /> type.
		/// </summary>
		/// <param name="persistenceOptions"></param>
		public DefaultDatabaseConnectionStringProvider(IOptions<PersistenceOptions> persistenceOptions)
		{
			this.Options = persistenceOptions.Value;
		}

		/// <summary>
		///     Gets the persistence options.
		/// </summary>
		protected PersistenceOptions Options { get; }

		/// <inheritdoc />
		public virtual string GetConnectionString(RepositoryName repositoryName)
		{
			// No tenant is needed to create the connection string.
			// Use the default connection string, if the aggregate belongs to it.
			RepositoryOptions repositoryOptions = this.Options.Repositories[repositoryName.Name];

			// Get the connection string for the repository.
			string connectionString = this.Options.ConnectionStrings[repositoryOptions.ConnectionStringName];

			return connectionString;
		}
	}
}
