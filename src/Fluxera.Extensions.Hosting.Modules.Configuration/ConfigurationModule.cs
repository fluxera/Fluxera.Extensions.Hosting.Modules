namespace Fluxera.Extensions.Hosting.Modules.Configuration
{
	using Fluxera.Extensions.DependencyInjection;
	using JetBrains.Annotations;

	/// <summary>
	///     A module that enables configuration.
	/// </summary>
	[PublicAPI]
	public sealed class ConfigurationModule : ConfigureServicesModule
	{
		/// <inheritdoc />
		public override void PreConfigureServices(IServiceConfigurationContext context)
		{
			// Add the contributor list.
			context.Log("AddObjectAccessor(ConfigureContributorList)",
				services => services.AddObjectAccessor(new ConfigureContributorList(), ObjectAccessorLifetime.ConfigureServices));
		}

		/// <inheritdoc />
		public override void ConfigureServices(IServiceConfigurationContext context)
		{
			// Configure the module options.
			ConfigureContributorList contributorList = context.Services.GetObject<ConfigureContributorList>();
			foreach(IConfigureOptionsContributor contributor in contributorList)
			{
				contributor.Configure(context);
			}
		}
	}
}
