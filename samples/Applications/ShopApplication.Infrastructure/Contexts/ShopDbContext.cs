namespace ShopApplication.Infrastructure.Contexts
{
	using Catalog.Domain.ProductAggregate;
	using Catalog.Domain.Shared.ProductAggregate;
	using Catalog.Infrastructure.Contexts;
	using Fluxera.Extensions.Hosting.Modules.Persistence;
	using Fluxera.Repository;
	using Fluxera.Repository.EntityFrameworkCore;
	using Fluxera.Utilities.Extensions;
	using MassTransit;
	using Microsoft.EntityFrameworkCore;
	using Ordering.Infrastructure.Contexts;

	public sealed class ShopDbContext : DbContext
	{
		private readonly IDatabaseConnectionStringProvider databaseConnectionStringProvider;
		private readonly IDatabaseNameProvider databaseNameProvider;

		public ShopDbContext()
		{
		}

		public ShopDbContext(
			DbContextOptions<ShopDbContext> options,
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
			// Add the Catalog component domain entities.
			modelBuilder.AddProductEntity(builder =>
			{
				builder.HasData(
					new Product
					{
						ID = new ProductId("f2f764f2-3690-4abb-9bd9-58bc8502919a"),
						Name = "Soap",
						Price = 3.95m
					},
					new Product
					{
						ID = new ProductId("bc04d38d-22ce-4861-98f3-79786ffa8f1e"),
						Name = "Deodorant",
						Price = 5.95m
					},
					new Product
					{
						ID = new ProductId("ee48daf6-e983-4b5a-ba11-4d15f0bd3840"),
						Name = "Shower Gel",
						Price = 5.99m
					});
			});

			// Add the Ordering component domain entities.
			modelBuilder.AddOrderEntity();
			modelBuilder.AddOrderItemEntity();
			modelBuilder.AddCustomerEntity();

			// Add the entities for the transactional inbox/outbox.
			modelBuilder.AddInboxStateEntity(builder =>
			{
				builder.ToTable("InboxStates");
			});
			modelBuilder.AddOutboxMessageEntity(builder =>
			{
				builder.ToTable("OutboxMessages");
			});
			modelBuilder.AddOutboxStateEntity(builder =>
			{
				builder.ToTable("OutboxStates");
			});
		}
	}
}
