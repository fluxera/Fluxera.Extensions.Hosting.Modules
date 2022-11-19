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

		public Action<IRepositoryBuilder, string, Type, Action<IRepositoryOptionsBuilder>, IServiceConfigurationContext> AddRepository
		{
			get
			{
				return (builder, repositoryName, contextType, configureAction, context) =>
				{
					context.Log("AddInMemoryRepository",
						_ => builder.AddInMemoryRepository(repositoryName, contextType, configureAction));
				};
			}
		}
	}
}
