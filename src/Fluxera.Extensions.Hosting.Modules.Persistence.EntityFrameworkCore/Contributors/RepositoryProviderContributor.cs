namespace Fluxera.Extensions.Hosting.Modules.Persistence.EntityFrameworkCore.Contributors
{
	using System;
	using Fluxera.Extensions.Hosting.Modules.Configuration;
	using Fluxera.Guards;
	using Fluxera.Repository;
	using Fluxera.Repository.EntityFrameworkCore;
	using JetBrains.Annotations;

	[UsedImplicitly]
	internal sealed class RepositoryProviderContributor : IRepositoryProviderContributor
	{
		public string RepositoryProviderName => RepositoryProviderNames.EntityFrameworkCore;

		public Action<IRepositoryBuilder, string, Type, Action<IRepositoryOptionsBuilder>, IServiceConfigurationContext> AddRepository
		{
			get
			{
				return (builder, repositoryName, contextType, optionsAction, context) =>
				{
					EntityFrameworkCorePersistenceOptions persistenceOptions = context.Services.GetOptions<EntityFrameworkCorePersistenceOptions>();
					EntityFrameworkCoreRepositoryOptions repositoryOptions = persistenceOptions.Repositories.GetOptionsOrDefault(repositoryName);

					Type dbContextType = Type.GetType(repositoryOptions.DbContextType);
					Guard.Against.Null(dbContextType, message: $"The db context must be configured for EFCore repository '{repositoryName}'.");

					// Add the db context.
					context.Services.AddDbContext(dbContextType);

					context.Log("AddEntityFrameworkRepository",
						_ => builder.AddEntityFrameworkRepository(repositoryName, contextType, optionsAction));
				};
			}
		}
	}
}
