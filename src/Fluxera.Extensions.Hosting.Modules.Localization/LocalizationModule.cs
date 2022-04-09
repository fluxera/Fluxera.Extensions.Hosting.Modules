namespace Fluxera.Extensions.Hosting.Modules.Localization
{
	using Fluxera.Extensions.Hosting.Modules.Configuration;
	using JetBrains.Annotations;
	using Microsoft.Extensions.DependencyInjection;

	/// <summary>
	///     A module that enables localization.
	/// </summary>
	/// <remarks>
	///     https://joonasw.net/view/aspnet-core-localization-deep-dive
	/// </remarks>
	[PublicAPI]
	[DependsOn(typeof(ConfigurationModule))]
	public sealed class LocalizationModule : ConfigureServicesModule
	{
		/// <inheritdoc />
		public override void ConfigureServices(IServiceConfigurationContext context)
		{
			// Add localization.
			context.Log("AddLocalization", services =>
			{
				LocalizationOptions localizationOptions = context.Configuration.GetForModule<LocalizationOptions>("Localization");

				services.AddLocalization(options =>
				{
					// We will put our translations in a folder called Resources.
					options.ResourcesPath = localizationOptions.ResourcesPath;
				});
			});
		}
	}
}
