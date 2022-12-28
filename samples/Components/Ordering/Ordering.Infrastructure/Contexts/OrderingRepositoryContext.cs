#undef EFCORE
#define MONGO

namespace Ordering.Infrastructure.Contexts
{
	using Fluxera.Extensions.Hosting.Modules.Persistence;
	using Fluxera.Repository;
	using Fluxera.Repository.MongoDB;
	using JetBrains.Annotations;

#if EFCORE
	[PublicAPI]
	internal sealed class OrderingRepositoryContext : EntityFrameworkCoreContext
	{
		/// <inheritdoc />
		protected override void ConfigureOptions(EntityFrameworkCoreContextOptions options)
		{
			options.UseDbContext(typeof(OrderingDbContext));
		}
	}
#elif MONGO
	[PublicAPI]
	public sealed class OrderingRepositoryContext : MongoContext
	{
		private readonly IDatabaseConnectionStringProvider databaseConnectionStringProvider;
		private readonly IDatabaseNameProvider databaseNameProvider;

		public OrderingRepositoryContext(
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
