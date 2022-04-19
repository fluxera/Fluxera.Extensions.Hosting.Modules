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

		public Action<IRepositoryBuilder, string, Action<IRepositoryOptionsBuilder>> AddRepository
		{
			get
			{
				return (builder, repositoryName, optionsAction) =>
				{
					builder.AddMongoRepository(repositoryName, optionsAction, conventions =>
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
					builder
						.AddSetting("Mongo.ConnectionString", connectionString)
						.AddSetting("Mongo.UseSsl", connectionString.Contains("ssl=true"))
						.AddSetting("Mongo.Database", options.DatabaseName);
				};
			}
		}
	}
}
