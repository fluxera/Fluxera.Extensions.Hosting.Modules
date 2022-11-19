namespace Fluxera.Extensions.Hosting.Modules.Persistence.EntityFrameworkCore.Contributors
{
	using System;
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
					context.Log("AddEntityFrameworkRepository",
						_ => builder.AddEntityFrameworkRepository(repositoryName, contextType, optionsAction));
				};
			}
		}
	}
}
