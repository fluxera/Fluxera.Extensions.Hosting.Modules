namespace Fluxera.Extensions.Hosting.Modules.Configuration
{
	using Fluxera.Extensions.DependencyInjection;
	using JetBrains.Annotations;
	using Microsoft.Extensions.Configuration;
	using Microsoft.Extensions.DependencyInjection;

	/// <summary>
	///     A module that enables configuration.
	/// </summary>
	[PublicAPI]
	public sealed class ConfigurationModule : ConfigureServicesModule
	{
		/// <inheritdoc />
		public override void PreConfigureServices(IServiceConfigurationContext context)
		{
			// Configure the options.
			context.Log("Configure(HostingOptions)",
				services => services.Configure<HostingOptions>(context.Configuration.GetSection("Hosting")));

			// Add the options to the services.
			IConfigurationSection section = context.Configuration.GetSection("Hosting");
			HostingOptions hostingOptions = section.Get<HostingOptions>();

			context.Log("AddObjectAccessor(HostingOptions)",
				services => services.AddObjectAccessor(hostingOptions, ObjectAccessorLifetime.ConfigureServices));

			// Add the contributor list.
			context.Log("AddObjectAccessor(ConfigureContributorList)",
				services => services.AddObjectAccessor(new ConfigureContributorList(), ObjectAccessorLifetime.ConfigureServices));
		}

		/// <inheritdoc />
		public override void PostConfigureServices(IServiceConfigurationContext context)
		{
			// Configure the module options.
			ConfigureContributorList contributorList = context.Services.GetObject<ConfigureContributorList>();
			foreach(IConfigureContributor contributor in contributorList)
			{
				IConfigurationSection section = context.Configuration.GetSection($"Hosting:Modules:{contributor.Name}");
				context.Log($"Configure({contributor.OptionsType.Name})",
					services => contributor.Configure(services, section));
			}
		}
	}
}
