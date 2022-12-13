namespace Example.Infrastructure
{
	using Fluxera.Extensions.Hosting.Modules.Persistence;
	using Fluxera.Repository;
	using Fluxera.Repository.EntityFrameworkCore;
	using Fluxera.Utilities.Extensions;
	using MassTransit;
	using Microsoft.EntityFrameworkCore;
	using Microsoft.EntityFrameworkCore.Diagnostics;

	public sealed class ExampleDbContext : DbContext
	{
		private readonly IDatabaseConnectionStringProvider databaseConnectionStringProvider;
		private readonly IDatabaseNameProvider databaseNameProvider;

		public ExampleDbContext()
		{
		}

		public ExampleDbContext(
			DbContextOptions<ExampleDbContext> options,
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
			// System.InvalidOperationException: An error was generated for warning 'Microsoft.EntityFrameworkCore.Database.Transaction.TransactionIgnoredWarning':
			// Transactions are not supported by the in-memory store. See http://go.microsoft.com/fwlink/?LinkId=800142 This exception can be suppressed or logged
			// by passing event ID 'InMemoryEventId.TransactionIgnoredWarning' to the 'ConfigureWarnings' method in 'DbContext.OnConfiguring' or 'AddDbContext'.

			if(!optionsBuilder.IsConfigured)
			{
				RepositoryName repositoryName = new RepositoryName("Default");

				string databaseName = this.databaseNameProvider?.GetDatabaseName(repositoryName);
				string connectionString = this.databaseConnectionStringProvider?.GetConnectionString(repositoryName);

				connectionString ??= "Server=localhost;Integrated Security=True;TrustServerCertificate=True;";
				connectionString = connectionString.EnsureEndsWith(";");
				connectionString += $"Database={databaseName ?? "demo-database"}";

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

				entity.UseRepositoryDefaults();
			});

			modelBuilder.AddInboxStateEntity();
			modelBuilder.AddOutboxMessageEntity();
			modelBuilder.AddOutboxStateEntity();
		}
	}
}
