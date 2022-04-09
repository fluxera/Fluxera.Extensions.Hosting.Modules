namespace Fluxera.Extensions.Hosting.Modules.Caching.Redis.Contributors
{
	using Fluxera.Extensions.DataManagement;
	using Fluxera.Extensions.Hosting.Modules.Configuration;
	using Microsoft.Extensions.Configuration;
	using Microsoft.Extensions.DependencyInjection;

	internal sealed class ConfigureOptionsContributor : IConfigureOptionsContributor
	{
		/// <inheritdoc />
		public string Name => "Caching";

		/// <inheritdoc />
		public void Configure(IServiceConfigurationContext context, IConfigurationSection section)
		{
			IConfigurationSection connectionStringsSection = context.Configuration.GetSection("ConnectionStrings");
			ConnectionStrings connectionStrings = connectionStringsSection.Get<ConnectionStrings>();

			context.Services.Configure<CachingRedisOptions>(section.GetSection("Redis"));
			context.Services.Configure<CachingRedisOptions>(options =>
			{
				options.ConnectionStrings = connectionStrings;
			});
		}
	}
}
