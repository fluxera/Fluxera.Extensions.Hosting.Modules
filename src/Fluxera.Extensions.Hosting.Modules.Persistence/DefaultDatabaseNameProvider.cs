namespace Fluxera.Extensions.Hosting.Modules.Persistence
{
	using Fluxera.Repository;
	using Fluxera.Utilities.Extensions;
	using JetBrains.Annotations;
	using Microsoft.Extensions.Options;

	/// <summary>
	///     A default database name provider that gets the name from configuration.
	/// </summary>
	[PublicAPI]
	public class DefaultDatabaseNameProvider : IDatabaseNameProvider
	{
		/// <summary>
		///     Crates a new instance of the <see cref="DefaultDatabaseNameProvider" /> type.
		/// </summary>
		/// <param name="persistenceOptions"></param>
		public DefaultDatabaseNameProvider(IOptions<PersistenceOptions> persistenceOptions)
		{
			this.Options = persistenceOptions.Value;
		}

		/// <summary>
		///     Gets the persistence options.
		/// </summary>
		protected PersistenceOptions Options { get; }

		/// <inheritdoc />
		public virtual string GetDatabaseName(RepositoryName repositoryName)
		{
			// No tenant is needed to create the database name.
			// Use the default database name, if the aggregate belongs to it.
			RepositoryOptions repositoryOptions = this.Options.Repositories[repositoryName.Name];

			string databaseName = repositoryOptions.DatabaseNamePrefix.IsNullOrWhiteSpace()
				? repositoryOptions.DatabaseName
				: $"{repositoryOptions.DatabaseNamePrefix}-{repositoryOptions.DatabaseName}";

			return databaseName;
		}
	}
}
