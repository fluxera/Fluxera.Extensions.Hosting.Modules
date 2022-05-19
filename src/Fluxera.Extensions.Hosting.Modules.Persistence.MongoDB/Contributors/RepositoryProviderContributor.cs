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

		public Action<IRepositoryBuilder, string, Action<IRepositoryOptionsBuilder>, IServiceConfigurationContext> AddRepository
		{
			get
			{
				return (builder, repositoryName, optionsAction, context) =>
				{
					context.Log("AddMongoRepository",
						_ => builder.AddMongoRepository(repositoryName, optionsAction));
				};
			}
		}

		public Action<IRepositoryOptionsBuilder, string, RepositoryOptions, IServiceConfigurationContext> ConfigureRepository
		{
			get
			{
				return (builder, connectionString, options, _) =>
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
