namespace Ordering.Infrastructure.Contexts
{
	using Fluxera.Extensions.Hosting.Modules.Persistence;
	using Fluxera.Repository;
	using Fluxera.Repository.EntityFrameworkCore;
	using Fluxera.Utilities.Extensions;
	using MassTransit;
	using Microsoft.EntityFrameworkCore;
	using Ordering.Domain.CustomerAggregate;
	using Ordering.Domain.OrderAggregate;

	internal sealed class OrderingDbContext : DbContext
	{
		private readonly IDatabaseConnectionStringProvider databaseConnectionStringProvider;
		private readonly IDatabaseNameProvider databaseNameProvider;

		public OrderingDbContext()
		{
		}

		public OrderingDbContext(
			DbContextOptions<OrderingDbContext> options,
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
				connectionString += $"Database={databaseName ?? "demo-database"}";

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

			// Add the entities for the transactional inbox/outbox.
			modelBuilder.AddInboxStateEntity();
			modelBuilder.AddOutboxMessageEntity();
			modelBuilder.AddOutboxStateEntity();
		}
	}
}
