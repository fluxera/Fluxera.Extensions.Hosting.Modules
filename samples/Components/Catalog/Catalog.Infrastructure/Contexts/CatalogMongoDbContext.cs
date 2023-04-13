namespace Catalog.Infrastructure.Contexts
{
	using Fluxera.Extensions.Hosting.Modules.Persistence;
	using Fluxera.Repository;
	using MadEyeMatt.MongoDB.DbContext;

	internal sealed class CatalogMongoDbContext : MongoDbContext
	{
		private readonly IDatabaseConnectionStringProvider databaseConnectionStringProvider;
		private readonly IDatabaseNameProvider databaseNameProvider;

		public CatalogMongoDbContext(
			IDatabaseNameProvider databaseNameProvider = null,
			IDatabaseConnectionStringProvider databaseConnectionStringProvider = null)
		{
			this.databaseNameProvider = databaseNameProvider;
			this.databaseConnectionStringProvider = databaseConnectionStringProvider;
		}

		/// <inheritdoc />
		protected override void OnConfiguring(MongoDbContextOptionsBuilder builder)
		{
			RepositoryName repositoryName = new RepositoryName("Default");

			string databaseName = this.databaseNameProvider?.GetDatabaseName(repositoryName);
			string connectionString = this.databaseConnectionStringProvider?.GetConnectionString(repositoryName);

			builder.UseDatabase(connectionString, databaseName);
		}
	}
}
