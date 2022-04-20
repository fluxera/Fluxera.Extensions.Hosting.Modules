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

		public Action<IRepositoryBuilder, string, Action<IRepositoryOptionsBuilder>> AddRepository
		{
			get
			{
				return (builder, repositoryName, optionsAction) =>
				{
					builder.AddEntityFrameworkRepository(repositoryName, optionsAction);
				};
			}
		}

		public Action<IRepositoryOptionsBuilder, string, RepositoryOptions> ConfigureRepository
		{
			get
			{
				return (builder, connectionString, repositoryOptions) =>
				{
					bool logSQL = false;
					if(repositoryOptions.Settings.TryGetValue("LogSQL", out string setting))
					{
						logSQL = bool.Parse(setting);
					}

					Type dbContextType = null;

					builder
						.AddSetting("EntityFrameworkCore.DbContext", connectionString)
						.AddSetting("EntityFrameworkCore.ConnectionString", connectionString)
						.AddSetting("EntityFrameworkCore.LogSQL", logSQL);
				};
			}
		}
	}
}
