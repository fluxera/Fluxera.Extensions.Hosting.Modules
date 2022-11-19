namespace Fluxera.Extensions.Hosting.Modules.Persistence.LiteDB.Contributors
{
	using System;
	using Fluxera.Repository;
	using Fluxera.Repository.LiteDB;
	using JetBrains.Annotations;

	[UsedImplicitly]
	internal sealed class RepositoryProviderContributor : IRepositoryProviderContributor
	{
		public string RepositoryProviderName => RepositoryProviderNames.InMemory;

		public Action<IRepositoryBuilder, string, Type, Action<IRepositoryOptionsBuilder>, IServiceConfigurationContext> AddRepository
		{
			get
			{
				return (builder, repositoryName, contextType, optionsAction, context) =>
				{
					context.Log("AddLiteRepository",
						_ => builder.AddLiteRepository(repositoryName, contextType, optionsAction));
				};
			}
		}
	}
}
