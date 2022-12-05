namespace Example.Infrastructure
{
	using Fluxera.Extensions.Hosting.Modules.Persistence;
	using Fluxera.Repository;
	using Fluxera.Repository.EntityFrameworkCore;
	using MassTransit;
	using Microsoft.EntityFrameworkCore;
	using Microsoft.Extensions.Logging;

	public sealed class ExampleDbContext : DbContext
	{
		private readonly ILogger<ExampleDbContext> logger;
		private readonly IDatabaseConnectionStringProvider databaseConnectionStringProvider;
		private readonly IDatabaseNameProvider databaseNameProvider;

		public ExampleDbContext(
			DbContextOptions<ExampleDbContext> options,
			ILogger<ExampleDbContext> logger,
			IDatabaseNameProvider databaseNameProvider = null,
			IDatabaseConnectionStringProvider databaseConnectionStringProvider = null)
			: base(options)
		{
			this.logger = logger;
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

				this.logger.LogInformation("Using database name: '{DatabaseName}'.", databaseName);
				this.logger.LogInformation("Using connection string: '{ConnectionString}'.", connectionString);

				optionsBuilder.UseInMemoryDatabase(databaseName ?? "default");
			}
		}

		/// <inheritdoc />
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.AddInboxStateEntity();
			modelBuilder.AddOutboxMessageEntity();
			modelBuilder.AddOutboxStateEntity();

			modelBuilder.Entity<Domain.Example.Example>(entity =>
			{
				entity.ToTable("Examples");
			});

			modelBuilder.UseRepositoryDefaults();
		}
	}
}
