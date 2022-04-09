namespace Fluxera.Extensions.Hosting.Modules.Caching
{
	using Fluxera.Extensions.Common;
	using Fluxera.Extensions.Hosting.Modules.Caching.Contributors;
	using Fluxera.Extensions.Hosting.Modules.Configuration;
	using JetBrains.Annotations;
	using Microsoft.Extensions.DependencyInjection;

	/// <summary>
	///     A module that enables caching.
	/// </summary>
	[PublicAPI]
	[DependsOn(typeof(ConfigurationModule))]
	public sealed class CachingModule : ConfigureServicesModule
	{
		/// <inheritdoc />
		public override void PreConfigureServices(IServiceConfigurationContext context)
		{
			// Add the configure options contributor.
			context.Services.AddConfigureOptionsContributor<ConfigureOptionsContributor>();
		}

		/// <inheritdoc />
		public override void ConfigureServices(IServiceConfigurationContext context)
		{
			// Add cache services. 
			context.Log(services => services.AddMemoryCache());
			context.Log(services => services.AddDistributedMemoryCache());

			// Ass jitter service.
			context.Log(services => services.AddJitterCalculator());
		}
	}
}
