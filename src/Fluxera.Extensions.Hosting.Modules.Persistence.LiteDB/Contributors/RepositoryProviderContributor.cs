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
					context.Log("AddLiteRepository",
						_ => builder.AddLiteRepository(repositoryName, optionsAction));
				};
			}
		}

		public Action<IRepositoryOptionsBuilder, string, RepositoryOptions, IServiceConfigurationContext> ConfigureRepository
		{
			get
			{
				return (builder, connectionString, _, _) =>
				{
					builder.AddSetting("Lite.Database", connectionString);
				};
			}
		}
	}
}
