namespace Fluxera.Extensions.Hosting.Modules.Persistence.MongoDB.Contributors
{
	using System;
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
					context.Log("AddMongoRepository",
						_ => builder.AddMongoRepository(repositoryName, contextType, optionsAction));
				};
			}
		}
	}
}
