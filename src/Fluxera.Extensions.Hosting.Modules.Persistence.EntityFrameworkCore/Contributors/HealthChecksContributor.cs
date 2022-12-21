namespace Fluxera.Extensions.Hosting.Modules.Persistence.EntityFrameworkCore.Contributors
{
	using System;
	using System.Linq;
	using System.Reflection;
	using Fluxera.Extensions.Hosting.Modules.Configuration;
	using Fluxera.Extensions.Hosting.Modules.HealthChecks;
	using Fluxera.Guards;
	using Microsoft.Extensions.DependencyInjection;

	internal sealed class HealthChecksContributor : IHealthChecksContributor
	{
		/// <inheritdoc />
		public void ConfigureHealthChecks(IHealthChecksBuilder builder, IServiceConfigurationContext context)
		{
			EntityFrameworkCorePersistenceOptions persistenceOptions = context.Services.GetOptions<EntityFrameworkCorePersistenceOptions>();
			foreach((string key, EntityFrameworkCoreRepositoryOptions repositoryOptions) in persistenceOptions.Repositories)
			{
				if(repositoryOptions.ProviderName == RepositoryProviderNames.EntityFrameworkCore)
				{
					Type dbContextType = Type.GetType(repositoryOptions.DbContextType);
					dbContextType = Guard.Against.Null(dbContextType, message: $"The db context must be configured for EFCore repository '{key}'.");

					MethodInfo methodInfo = typeof(EntityFrameworkCoreHealthChecksBuilderExtensions)
						.GetRuntimeMethods()
						.Single(x => x.Name == "AddDbContextCheck");

					string[] tags =
					{
						HealthCheckTags.Ready
					};

					//https://www.michalbialecki.com/en/2020/03/13/entity-framework-core-health-check/
					//builder.AddDbContextCheck($"EntityFrameworkCore-{key}", tags: tags);

					methodInfo
						.MakeGenericMethod(dbContextType)
						.Invoke(null, new object[]
						{
							builder,
							$"EntityFrameworkCore-{key}",
							null,
							tags,
							null
						});
				}
			}
		}
	}
}
