﻿namespace Fluxera.Extensions.Hosting.Modules.DataManagement
{
	using Fluxera.Extensions.DataManagement;
	using Fluxera.Extensions.DependencyInjection;
	using Fluxera.Extensions.Hosting.Modules.Configuration;
	using Fluxera.Extensions.Hosting.Modules.DataManagement.Contributors;
	using Fluxera.Utilities;
	using JetBrains.Annotations;
	using Microsoft.Extensions.DependencyInjection;
	using Microsoft.Extensions.DependencyInjection.Extensions;
	using Microsoft.Extensions.Hosting;

	/// <summary>
	///     A module that enables data management.
	/// </summary>
	[PublicAPI]
	[DependsOn(typeof(ConfigurationModule))]
	public sealed class DataManagementModule : ConfigureApplicationModule
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
			// Add the default connection string resolver.
			context.Log("AddConnectionStringResolver",
				services => services.TryAddTransient<IConnectionStringResolver, DefaultConnectionStringResolver>());

			// Add the contributor list.
			context.Log("AddObjectAccessor(DataSeedingContributorList)",
				services => services.AddObjectAccessor(new DataSeedingContributorList(), ObjectAccessorLifetime.Configure));
		}

		/// <inheritdoc />
		public override void PostConfigure(IApplicationInitializationContext context)
		{
			IHostEnvironment environment = context.ServiceProvider.GetRequiredService<IHostEnvironment>();

			// We only support the data seeders in non-production environments.
			if(!environment.IsProduction())
			{
				context.Log("ExecuteDataSeeding", serviceProvider =>
				{
					DataSeedingContributorList contributorList = serviceProvider.GetObject<DataSeedingContributorList>();
					AsyncHelper.RunSync(async () =>
					{
						foreach(IDataSeedingContributor contributor in contributorList)
						{
							await contributor.ExecuteAsync(context);
						}
					});
				});
			}
		}
	}
}
