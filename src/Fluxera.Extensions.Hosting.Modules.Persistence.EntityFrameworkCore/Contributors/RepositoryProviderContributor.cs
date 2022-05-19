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

		public Action<IRepositoryBuilder, string, Action<IRepositoryOptionsBuilder>, IServiceConfigurationContext> AddRepository
		{
			get
			{
				return (builder, repositoryName, optionsAction, context) =>
				{
					builder.AddEntityFrameworkRepository(repositoryName, optionsAction);
				};
			}
		}

		public Action<IRepositoryOptionsBuilder, string, RepositoryOptions, IServiceConfigurationContext> ConfigureRepository
		{
			get
			{
				return (builder, connectionString, repositoryOptions, context) =>
				{
					builder
						//.AddSetting("EntityFrameworkCore.DbContext", connectionString)
						.AddSetting("EntityFrameworkCore.ConnectionString", connectionString);
				};
			}
		}
	}
}
