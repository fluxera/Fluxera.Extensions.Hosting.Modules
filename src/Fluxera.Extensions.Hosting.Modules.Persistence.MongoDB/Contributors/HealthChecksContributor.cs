namespace Fluxera.Extensions.Hosting.Modules.Persistence.MongoDB.Contributors
{
	using Fluxera.Extensions.Hosting.Modules.Configuration;
	using Fluxera.Extensions.Hosting.Modules.HealthChecks;
	using Fluxera.Extensions.Hosting.Modules.Persistence;
	using Microsoft.Extensions.DependencyInjection;

	internal sealed class HealthChecksContributor : IHealthChecksContributor
	{
		/// <inheritdoc />
		public void ConfigureHealthChecks(IHealthChecksBuilder builder, IServiceConfigurationContext context)
		{
			PersistenceOptions persistenceOptions = context.Services.GetOptions<PersistenceOptions>();
			foreach((string key, RepositoryOptions repositoryOptions) in persistenceOptions.Repositories)
			{
				if(repositoryOptions.ProviderName == RepositoryProviderNames.MongoDB)
				{
					string connectionString = persistenceOptions.ConnectionStrings[repositoryOptions.ConnectionStringName];

					// https://github.com/Xabaril/AspNetCore.Diagnostics.HealthChecks/tree/master/src/HealthChecks.MongoDb
					builder.AddMongoDb(connectionString, name: $"RabbitMQ-{key}", tags: new string[]
					{
						HealthCheckTags.Ready
					});
				}
			}
		}
	}
}
