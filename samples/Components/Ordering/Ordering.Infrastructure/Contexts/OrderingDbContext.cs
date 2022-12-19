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

	public sealed class OrderingDbContext : DbContext
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
			modelBuilder.Entity<Order>(entity =>
			{
				entity.ToTable("Orders");

				entity.OwnsOne(x => x.ShippingAddress);

				entity.OwnsOne(x => x.BillingAddress);

				entity.HasMany(x => x.OrderItems);

				entity.UseRepositoryDefaults();
			});

			modelBuilder.Entity<OrderItem>(entity =>
			{
				entity.ToTable("OrderItems");

				entity.UseRepositoryDefaults();
			});

			modelBuilder.Entity<Customer>(entity =>
			{
				entity.ToTable("Customers");

				entity.OwnsOne(x => x.Name);

				entity.UseRepositoryDefaults();
			});

			modelBuilder.AddInboxStateEntity();
			modelBuilder.AddOutboxMessageEntity();
			modelBuilder.AddOutboxStateEntity();
		}
	}
}
