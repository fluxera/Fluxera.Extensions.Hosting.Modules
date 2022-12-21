namespace Fluxera.Extensions.Hosting.Modules.Caching.Redis.Contributors
{
	using Fluxera.Extensions.Hosting.Modules.Configuration;
	using Fluxera.Extensions.Hosting.Modules.HealthChecks;
	using Microsoft.Extensions.DependencyInjection;

	internal sealed class HealthChecksContributor : IHealthChecksContributor
	{
		/// <inheritdoc />
		public void ConfigureHealthChecks(IHealthChecksBuilder builder, IServiceConfigurationContext context)
		{
			RedisCachingOptions cachingOptions = context.Services.GetOptions<RedisCachingOptions>();
			string connectionString = cachingOptions.ConnectionStrings[cachingOptions.ConnectionStringName];

			builder.AddRedis(connectionString, "Redis", tags: new string[]
			{
				HealthCheckTags.Ready
			});
		}
	}
}
