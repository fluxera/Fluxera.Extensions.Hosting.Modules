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

		public Action<IRepositoryBuilder, string, Action<IRepositoryOptionsBuilder>> AddRepository
		{
			get
			{
				return (builder, repositoryName, optionsAction) =>
				{
					builder.AddLiteRepository(repositoryName, optionsAction, mapper =>
					{
						// TODO: How do we get this special configuration in here?
					});
				};
			}
		}

		public Action<IRepositoryOptionsBuilder, string, RepositoryOptions> ConfigureRepository
		{
			get
			{
				return (builder, connectionString, options) =>
				{
					builder.AddSetting("Lite.Database", connectionString);
				};
			}
		}
	}
}
