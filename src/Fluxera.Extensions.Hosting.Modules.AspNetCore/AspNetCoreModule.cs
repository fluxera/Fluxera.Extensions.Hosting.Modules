namespace Fluxera.Extensions.Hosting.Modules.AspNetCore
{
	using Fluxera.Extensions.Hosting.Modules.Configuration;
	using JetBrains.Annotations;
	using Microsoft.Extensions.DependencyInjection;

	/// <summary>
	///     A module that enables ASP.NET Core basic features.
	/// </summary>
	[PublicAPI]
	[DependsOn(typeof(ConfigurationModule))]
	public sealed class AspNetCoreModule : ConfigureServicesModule
	{
		/// <inheritdoc />
		public override void ConfigureServices(IServiceConfigurationContext context)
		{
			// Add the http context accessor.
			context.Log("AddHttpContextAccessor",
				services => services.AddHttpContextAccessor());

			// Add the action context accessor.
			context.Log("AddActionContextAccessor",
				services => services.AddActionContextAccessor());
		}
	}
}
