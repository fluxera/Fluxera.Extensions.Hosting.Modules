namespace Fluxera.Extensions.Hosting.Modules.Caching.Redis
{
	using Fluxera.Extensions.DataManagement;
	using Fluxera.Extensions.DependencyInjection;
	using Fluxera.Extensions.Hosting.Modules.Caching.Redis.Contributors;
	using Fluxera.Extensions.Hosting.Modules.Configuration;
	using Fluxera.Extensions.Hosting.Modules.DataManagement;
	using Fluxera.Extensions.Hosting.Modules.OpenTelemetry;
	using JetBrains.Annotations;
	using Microsoft.Extensions.DependencyInjection;

	/// <summary>
	///     A module that enables Redis caching.
	/// </summary>
	[PublicAPI]
	[DependsOn(typeof(CachingModule))]
	[DependsOn(typeof(DataManagementModule))]
	[DependsOn(typeof(OpenTelemetryModule))]
	public sealed class RedisCachingModule : ConfigureServicesModule
	{
		/// <inheritdoc />
		public override void PreConfigureServices(IServiceConfigurationContext context)
		{
			// Add the configure options contributor.
			context.Services.AddConfigureOptionsContributor<ConfigureOptionsContributor>();

			// Add the tracer provider contributor.
			context.Services.AddTracerProviderContributor<TracerProviderContributor>();
		}

		/// <inheritdoc />
		public override void ConfigureServices(IServiceConfigurationContext context)
		{
			RedisCachingOptions redisOptions = context.Services.GetOptions<RedisCachingOptions>();
			redisOptions.ConnectionStrings = context.Services.GetObject<ConnectionStrings>();

			// Add the Redis cache services.
			context.Log("AddStackExchangeRedisCache", services =>
			{
				services.AddStackExchangeRedisCache(options =>
				{
					options.Configuration = redisOptions.ConnectionStrings[redisOptions.ConnectionStringName];
					options.InstanceName = redisOptions.InstanceName;
				});
			});
		}
	}
}
