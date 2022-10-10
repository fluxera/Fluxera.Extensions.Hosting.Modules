namespace Fluxera.Extensions.Hosting.Modules.Configuration
{
	using System.Reflection;
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
			Assembly assembly = Assembly.GetCallingAssembly();

			// Configure the options.
			context.Log("Configure(HostingOptions)",
				services =>
				{
					return services
						.Configure<HostingOptions>(context.Configuration.GetSection("Hosting"))
						.Configure<HostingOptions>(options =>
						{
							options.AppName = context.Environment.ApplicationName;
							options.Version = assembly.GetName().Version;
						});
				});

			// Add the options to the services.
			IConfigurationSection section = context.Configuration.GetSection("Hosting");
			HostingOptions hostingOptions = section.Get<HostingOptions>();
			hostingOptions.AppName = context.Environment.ApplicationName;
			hostingOptions.Version = assembly.GetName().Version;

			context.Log("AddObjectAccessor(HostingOptions)",
				services => services.AddObjectAccessor(hostingOptions, ObjectAccessorLifetime.ConfigureServices));

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
