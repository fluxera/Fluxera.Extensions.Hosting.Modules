namespace Example.Infrastructure
{
	using Fluxera.Extensions.Hosting.Modules.Persistence;
	using Fluxera.Repository;
	using Fluxera.Repository.InMemory;
	using JetBrains.Annotations;
	using Microsoft.Extensions.Logging;

	[PublicAPI]
	public sealed class ExampleContext : InMemoryContext
	{
		private readonly ILogger<ExampleContext> logger;
		private readonly IDatabaseConnectionStringProvider databaseConnectionStringProvider;
		private readonly IDatabaseNameProvider databaseNameProvider;

		public ExampleContext(
			ILogger<ExampleContext> logger,
			IDatabaseNameProvider databaseNameProvider = null,
			IDatabaseConnectionStringProvider databaseConnectionStringProvider = null)
		{
			this.logger = logger;
			this.databaseNameProvider = databaseNameProvider;
			this.databaseConnectionStringProvider = databaseConnectionStringProvider;
		}

		/// <inheritdoc />
		protected override void ConfigureOptions(InMemoryContextOptions options)
		{
			RepositoryName repositoryName = new RepositoryName("Default");

			string databaseName = this.databaseNameProvider?.GetDatabaseName(repositoryName);
			string connectionString = this.databaseConnectionStringProvider?.GetConnectionString(repositoryName);

			this.logger.LogInformation("Using database name: '{DatabaseName}'.", databaseName);
			this.logger.LogInformation("Using connection string: '{ConnectionString}'.", connectionString);

			options.Database = databaseName;
		}
	}
}
