namespace Fluxera.Extensions.Hosting.Modules.Caching.Redis.Contributors
{
	using Fluxera.Extensions.DataManagement;
	using Fluxera.Extensions.Hosting.Modules.Configuration;
	using Microsoft.Extensions.DependencyInjection;

	internal sealed class ConfigureOptionsContributor : ConfigureOptionsContributorBase<RedisCachingOptions>
	{
		/// <inheritdoc />
		public override string SectionName => "Caching:Redis";

		/// <inheritdoc />
		protected override void AdditionalConfigure(IServiceConfigurationContext context, RedisCachingOptions createdOptions)
		{
			createdOptions.ConnectionStrings = context.Services.GetOptions<ConnectionStrings>();

			context.Log("Configure(CachingRedisOptions)",
				services =>
				{
					services.Configure<RedisCachingOptions>(options =>
					{
						options.ConnectionStrings = createdOptions.ConnectionStrings;
					});
				});
		}
	}
}
