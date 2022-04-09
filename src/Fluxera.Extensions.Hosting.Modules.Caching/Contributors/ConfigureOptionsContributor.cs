namespace Fluxera.Extensions.Hosting.Modules.Caching.Contributors
{
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
			context.Services.Configure<CachingOptions>(section);
		}
	}
}
