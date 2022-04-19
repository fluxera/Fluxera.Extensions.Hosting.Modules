namespace Fluxera.Extensions.Hosting.Modules.Persistence.InMemory.Contributors
{
	using System;
	using Fluxera.Repository;
	using Fluxera.Repository.InMemory;
	using JetBrains.Annotations;

	[UsedImplicitly]
	internal sealed class RepositoryProviderContributor : IRepositoryProviderContributor
	{
		public Action<IRepositoryOptionsBuilder, string, RepositoryOptions> ConfigureRepository
		{
			get
			{
				return (builder, connectionString, options) =>
				{
				};
			}
		}

		public string RepositoryProviderName => RepositoryProviderNames.InMemory;

		public Action<IRepositoryBuilder, string, Action<IRepositoryOptionsBuilder>> AddRepository
		{
			get
			{
				return (builder, repositoryName, configureAction) =>
				{
					builder.AddInMemoryRepository(repositoryName, configureAction);
				};
			}
		}
	}
}
