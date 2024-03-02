namespace Fluxera.Extensions.Hosting.Modules.Scheduler.Persistent.Contributors
{
	using System;
	using Fluxera.Extensions.Hosting.Modules.Scheduler;
	using JetBrains.Annotations;
	using Microsoft.Extensions.Configuration;
	using Quartz;

	[UsedImplicitly]
	internal sealed class StoreContributor : IStoreContributor
	{
		/// <inheritdoc />
		public void ConfigureStore(IServiceCollectionQuartzConfigurator configurator, IServiceConfigurationContext context)
		{
			PersistentSchedulerOptions schedulerOptions = new PersistentSchedulerOptions();

			IConfigurationSection section = context.Configuration.GetSection("Scheduler:Store");
			section.Bind(schedulerOptions);

			string connectionString = context.Configuration.GetConnectionString(schedulerOptions.ConnectionStringName);
			if(string.IsNullOrWhiteSpace(connectionString))
			{
				throw new InvalidOperationException("No connection string for the scheduler available in the configuration.");
			}

			configurator.UsePersistentStore(options =>
			{
				options.PerformSchemaValidation = schedulerOptions.PerformSchemaValidation;
				options.RetryInterval = schedulerOptions.RetryInterval;
				options.UseProperties = schedulerOptions.UseProperties;

				switch(schedulerOptions.Database)
				{
					case DatabaseKind.SQLServer:
						options.UseSqlServer(connectionString);
						break;
					case DatabaseKind.MySQL:
						options.UseMySql(connectionString);
						break;
					case DatabaseKind.SQLite:
						options.UseMicrosoftSQLite(connectionString);
						break;
					default:
						throw new ArgumentOutOfRangeException(nameof(schedulerOptions.Database), "Unknown database kind.");
				}

				// TODO: Replace with STJ when 4.0 is Quartz released.
				options.UseNewtonsoftJsonSerializer();

				if(schedulerOptions.Database != DatabaseKind.SQLite)
				{
					options.UseClustering(clusterOptions =>
					{
						clusterOptions.CheckinMisfireThreshold = schedulerOptions.CheckinMisfireThreshold;
						clusterOptions.CheckinInterval = schedulerOptions.CheckinInterval;
					});
				}
			});
		}
	}
}
