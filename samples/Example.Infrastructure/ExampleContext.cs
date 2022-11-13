namespace Example.Infrastructure
{
	using Fluxera.Extensions.Hosting.Modules.Persistence;
	using Fluxera.Repository;
	using Fluxera.Repository.InMemory;

	public class ExampleContext : InMemoryContext
	{
		private readonly IDatabaseConnectionStringProvider databaseConnectionStringProvider;
		private readonly IDatabaseNameProvider databaseNameProvider;

		public ExampleContext(
			IDatabaseNameProvider databaseNameProvider = null,
			IDatabaseConnectionStringProvider databaseConnectionStringProvider = null)
		{
			this.databaseNameProvider = databaseNameProvider;
			this.databaseConnectionStringProvider = databaseConnectionStringProvider;
		}

		/// <inheritdoc />
		protected override void ConfigureOptions(InMemoryContextOptions options)
		{
			RepositoryName repositoryName = new RepositoryName("Default");
			options.Database = this.databaseNameProvider.GetDatabaseName(repositoryName);
		}
	}
}
