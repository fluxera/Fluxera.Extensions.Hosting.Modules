namespace Fluxera.Extensions.Hosting.Modules.Persistence.InMemory.Contributors
{
	using System;
	using Fluxera.Repository;
	using Fluxera.Repository.InMemory;
	using JetBrains.Annotations;

	[UsedImplicitly]
	internal sealed class RepositoryProviderContributor : IRepositoryProviderContributor
	{
		public string RepositoryProviderName => RepositoryProviderNames.InMemory;

		public Action<IRepositoryBuilder, string, Action<IRepositoryOptionsBuilder>, IServiceConfigurationContext> AddRepository
		{
			get
			{
				return (builder, repositoryName, configureAction, context) =>
				{
					context.Log("AddInMemoryRepository",
						_ => builder.AddInMemoryRepository(repositoryName, configureAction));
				};
			}
		}

		public Action<IRepositoryOptionsBuilder, string, RepositoryOptions, IServiceConfigurationContext> ConfigureRepository
		{
			get
			{
				return (_, _, _, _) =>
				{
				};
			}
		}
	}
}
