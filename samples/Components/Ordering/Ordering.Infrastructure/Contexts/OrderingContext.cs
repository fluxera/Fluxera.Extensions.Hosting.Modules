#undef EFCORE
#define MONGO

namespace Ordering.Infrastructure.Contexts
{
	using Fluxera.Extensions.Hosting.Modules.Persistence;
	using Fluxera.Repository;
	using JetBrains.Annotations;
	using MadEyeMatt.MongoDB.DbContext;

#if EFCORE
	[UsedImplicitly]
	internal sealed class OrderingContext : DbContext
	{
		private readonly IDatabaseConnectionStringProvider databaseConnectionStringProvider;
		private readonly IDatabaseNameProvider databaseNameProvider;

		public OrderingContext()
		{
		}

		public OrderingContext(
			DbContextOptions<OrderingContext> options,
			IDatabaseNameProvider databaseNameProvider = null,
			IDatabaseConnectionStringProvider databaseConnectionStringProvider = null)
			: base(options)
		{
			this.databaseNameProvider = databaseNameProvider;
			this.databaseConnectionStringProvider = databaseConnectionStringProvider;
		}

		/// <inheritdoc />
		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			if(!optionsBuilder.IsConfigured)
			{
				RepositoryName repositoryName = new RepositoryName("Default");

				string databaseName = this.databaseNameProvider?.GetDatabaseName(repositoryName);
				string connectionString = this.databaseConnectionStringProvider?.GetConnectionString(repositoryName);

				connectionString ??= "Server=localhost;Integrated Security=True;TrustServerCertificate=True;";
				connectionString = connectionString.EnsureEndsWith(";");
				connectionString += $"Database={databaseName ?? "shop"}";

				optionsBuilder.UseSqlServer(connectionString);
			}
		}

		/// <inheritdoc />
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			// Add the domain entities.
			modelBuilder.AddOrderEntity();
			modelBuilder.AddOrderItemEntity();
			modelBuilder.AddCustomerEntity();

			// TODO: Include when transactional Outbox is fixed.
			// Add the entities for the transactional inbox/outbox.
			//modelBuilder.AddInboxStateEntity(builder =>
			//{
			//	builder.ToTable("InboxStates");
			//});
			//modelBuilder.AddOutboxMessageEntity(builder =>
			//{
			//	builder.ToTable("OutboxMessages");
			//});
			//modelBuilder.AddOutboxStateEntity(builder =>
			//{
			//	builder.ToTable("OutboxStates");
			//});
		}
	}
#elif MONGO
	[UsedImplicitly]
	internal sealed class OrderingContext : MongoDbContext
	{
		private readonly IDatabaseConnectionStringProvider databaseConnectionStringProvider;
		private readonly IDatabaseNameProvider databaseNameProvider;

		public OrderingContext(
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
#endif
}
