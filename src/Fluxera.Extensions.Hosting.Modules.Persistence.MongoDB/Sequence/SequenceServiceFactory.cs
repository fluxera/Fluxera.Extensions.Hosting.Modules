namespace Fluxera.Extensions.Hosting.Modules.Persistence.MongoDB.Sequence
{
	using Fluxera.Repository;
	using JetBrains.Annotations;
	using Microsoft.Extensions.Options;

	[UsedImplicitly]
	internal sealed class SequenceServiceFactory : ISequenceServiceFactory
	{
		private readonly IDatabaseNameProvider databaseNameProvider;
		private readonly PersistenceOptions options;

		public SequenceServiceFactory(
			IOptions<PersistenceOptions> options,
			IDatabaseNameProvider databaseNameProvider)
		{
			this.options = options.Value;
			this.databaseNameProvider = databaseNameProvider;
		}

		public ISequenceService CreateSequenceService(RepositoryName repositoryName)
		{
			RepositoryOptions configuration = this.options.Repositories[(string)repositoryName];
			string connectionString = this.options.ConnectionStrings[configuration.ConnectionStringName];
			string databaseName = this.databaseNameProvider.GetDatabaseName(repositoryName);

			ISequenceService sequenceService = new SequenceService(connectionString, databaseName);
			return sequenceService;
		}
	}
}
