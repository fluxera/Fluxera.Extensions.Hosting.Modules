namespace Fluxera.Extensions.Hosting.Modules.Caching
{
	using Fluxera.Extensions.Common;
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
		public override void ConfigureServices(IServiceConfigurationContext context)
		{
			context.Log(services => services.AddJitterCalculator());
			context.Log(services => services.AddMemoryCache());
			context.Log(services => services.AddDistributedMemoryCache());

			//context.Services.Configure<CachingOptions>(options =>
			//{
			//	//options.GlobalCacheEntryOptions.SlidingExpiration = TimeSpan.FromMinutes(20);
			//});
		}
	}
}
