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

		public Action<IRepositoryBuilder, string, Action<IRepositoryOptionsBuilder>, IServiceConfigurationContext> AddRepository
		{
			get
			{
				return (builder, repositoryName, optionsAction, context) =>
				{
					builder.AddLiteRepository(repositoryName, optionsAction);
				};
			}
		}

		public Action<IRepositoryOptionsBuilder, string, RepositoryOptions, IServiceConfigurationContext> ConfigureRepository
		{
			get
			{
				return (builder, connectionString, options, context) =>
				{
					builder.AddSetting("Lite.Database", connectionString);
				};
			}
		}
	}
}
