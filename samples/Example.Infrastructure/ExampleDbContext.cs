namespace Example.Infrastructure
{
	using Fluxera.Extensions.Hosting.Modules.Persistence;
	using Fluxera.Repository;
	using Fluxera.Repository.EntityFrameworkCore;
	using MassTransit;
	using Microsoft.EntityFrameworkCore;
	using Microsoft.EntityFrameworkCore.Diagnostics;
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
			// System.InvalidOperationException: An error was generated for warning 'Microsoft.EntityFrameworkCore.Database.Transaction.TransactionIgnoredWarning':
			// Transactions are not supported by the in-memory store. See http://go.microsoft.com/fwlink/?LinkId=800142 This exception can be suppressed or logged
			// by passing event ID 'InMemoryEventId.TransactionIgnoredWarning' to the 'ConfigureWarnings' method in 'DbContext.OnConfiguring' or 'AddDbContext'.

			if(!optionsBuilder.IsConfigured)
			{
				RepositoryName repositoryName = new RepositoryName("Default");

				string databaseName = this.databaseNameProvider?.GetDatabaseName(repositoryName);
				string connectionStringEx = this.databaseConnectionStringProvider?.GetConnectionString(repositoryName);

				this.logger.LogInformation("Using database name: '{DatabaseName}'.", databaseName);
				this.logger.LogInformation("Using connection string: '{ConnectionString}'.", connectionStringEx);

				string connectionString =
					$"Server=MARS;Database={databaseName};Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
				optionsBuilder.UseSqlServer(connectionString);

				optionsBuilder.ConfigureWarnings(builder =>
				{
					builder.Log(InMemoryEventId.TransactionIgnoredWarning);
				});
			}
		}

		/// <inheritdoc />
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Domain.Example.Example>(entity =>
			{
				entity.ToTable("Examples");

				entity.OwnsOne(x => x.Detail);
			});

			modelBuilder.UseRepositoryDefaults();

			modelBuilder.AddInboxStateEntity();
			modelBuilder.AddOutboxMessageEntity();
			modelBuilder.AddOutboxStateEntity();
		}
	}
}
