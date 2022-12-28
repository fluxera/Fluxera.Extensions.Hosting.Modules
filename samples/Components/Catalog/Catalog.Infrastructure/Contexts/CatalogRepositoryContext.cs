#undef EFCORE
#define MONGO

namespace Catalog.Infrastructure.Contexts
{
	using Fluxera.Extensions.Hosting.Modules.Persistence;
	using Fluxera.Repository;
	using Fluxera.Repository.MongoDB;
	using JetBrains.Annotations;

#if EFCORE
	[PublicAPI]
	internal sealed class CatalogRepositoryContext : EntityFrameworkCoreContext
	{
		/// <inheritdoc />
		protected override void ConfigureOptions(EntityFrameworkCoreContextOptions options)
		{
			options.UseDbContext(typeof(CatalogDbContext));
		}
	}
#elif MONGO
	[PublicAPI]
	public sealed class CatalogRepositoryContext : MongoContext
	{
		private readonly IDatabaseConnectionStringProvider databaseConnectionStringProvider;
		private readonly IDatabaseNameProvider databaseNameProvider;

		public CatalogRepositoryContext(
			IDatabaseNameProvider databaseNameProvider = null,
			IDatabaseConnectionStringProvider databaseConnectionStringProvider = null)
		{
			this.databaseNameProvider = databaseNameProvider;
			this.databaseConnectionStringProvider = databaseConnectionStringProvider;
		}

		/// <inheritdoc />
		protected override void ConfigureOptions(MongoContextOptions options)
		{
			RepositoryName repositoryName = options.RepositoryName;

			string databaseName = this.databaseNameProvider?.GetDatabaseName(repositoryName);
			string connectionString = this.databaseConnectionStringProvider?.GetConnectionString(repositoryName);

			options.ConnectionString = connectionString;
			options.Database = databaseName;
		}
	}
#endif
}
