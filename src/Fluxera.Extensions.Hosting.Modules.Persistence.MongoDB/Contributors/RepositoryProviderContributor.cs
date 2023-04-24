namespace Fluxera.Extensions.Hosting.Modules.Persistence.MongoDB.Contributors
{
	using System;
	using Fluxera.Extensions.Hosting.Modules.Configuration;
	using Fluxera.Guards;
	using Fluxera.Repository;
	using Fluxera.Repository.MongoDB;
	using JetBrains.Annotations;

	[UsedImplicitly]
	internal sealed class RepositoryProviderContributor : IRepositoryProviderContributor
	{
		public string RepositoryProviderName => RepositoryProviderNames.MongoDB;

		public Action<IRepositoryBuilder, string, Type, Action<IRepositoryOptionsBuilder>, IServiceConfigurationContext> AddRepository
		{
			get
			{
				return (builder, repositoryName, contextType, optionsAction, context) =>
				{
					MongoPersistenceOptions persistenceOptions = context.Services.GetOptions<MongoPersistenceOptions>();
					MongoRepositoryOptions repositoryOptions = persistenceOptions.Repositories.GetOptionsOrDefault(repositoryName);

					Type dbContextType = Type.GetType(repositoryOptions.DbContextType);
					Guard.Against.Null(dbContextType, message: $"The mongo db context must be configured for MongoDB repository '{repositoryName}'.");

					// Add the mongo db context.
					context.Services.AddMongoDbContext(dbContextType);

					context.Log("AddMongoRepository",
						_ => builder.AddMongoRepository(repositoryName, contextType, optionsAction));
				};
			}
		}
	}
}
