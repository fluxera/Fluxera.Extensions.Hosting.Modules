namespace Fluxera.Extensions.Hosting.Modules.Scheduler
{
	using System;
	using System.Security.Cryptography;
	using System.Text;
	using Fluxera.Extensions.DependencyInjection;
	using Fluxera.Extensions.Hosting.Modules.Configuration;
	using Fluxera.Extensions.Hosting.Modules.HealthChecks;
	using Fluxera.Extensions.Hosting.Modules.OpenTelemetry;
	using Fluxera.Extensions.Hosting.Modules.Scheduler.Contributors;
	using Fluxera.Guards;
	using JetBrains.Annotations;
	using Microsoft.Extensions.Hosting;
	using Quartz;

	// TODO: When Quartz 4.0 get out:
	//  - Use System.Text.Json serializer (Quartz.Serialization.SystemTextJson)

	/// <summary>
	///     A module that enables Quartz scheduler support.
	/// </summary>
	[PublicAPI]
	[DependsOn(typeof(HealthChecksModule))]
	[DependsOn(typeof(OpenTelemetryModule))]
	[DependsOn(typeof(ConfigurationModule))]
	public sealed class SchedulerModule : ConfigureServicesModule
	{
		/// <inheritdoc />
		public override void PreConfigureServices(IServiceConfigurationContext context)
		{
			// Add the configure options contributor.
			context.Services.AddConfigureOptionsContributor<ConfigureOptionsContributor>();

			// Add the tracer provider contributor.
			context.Services.AddTracerProviderContributor<TracerProviderContributor>();

			// Add the health checks contributor.
			context.Services.AddHealthCheckContributor<HealthChecksContributor>();
		}

		/// <inheritdoc />
		public override void ConfigureServices(IServiceConfigurationContext context)
		{
			// Add the Quartz scheduler services.
			context.Log("AddQuartz", services =>
			{
				// Get the configured store contributor.
				IStoreContributor storeContributor = context.Services.GetObjectOrDefault<IStoreContributor>();
				if(storeContributor is null)
				{
					throw new InvalidOperationException("No store contributor was found.");
				}

				services.AddQuartz(configurator =>
				{
					configurator.SchedulerName = "Scheduler";
					configurator.SchedulerId = CreateSchedulerId(context);

					configurator.UseDefaultThreadPool(options =>
					{
						options.MaxConcurrency = 10;
					});

					configurator.UseTimeZoneConverter();

					// Add the store.
					storeContributor.ConfigureStore(configurator, context);
				});
			});

			context.Log("AddQuartzHostedService", 
				services => services.AddQuartzHostedService());
		}

		private static string CreateSchedulerId(IServiceConfigurationContext context)
		{
			string schedulerId = string.Empty;

			IHostEnvironment environment = context.Environment;
			if(environment != null)
			{
				schedulerId = $"{environment.EnvironmentName}-{environment.ApplicationName}-{Guid.NewGuid()}";
				schedulerId = ComputeHash(schedulerId);

				context.Logger.LogSchedulerIdentifier(schedulerId, environment.EnvironmentName, environment.ApplicationName);
			}

			return schedulerId;
		}

		private static string ComputeHash(string input)
		{
			Guard.Against.Null(input);

			// Step 1, calculate hash from input.
			using(HashAlgorithm algorithm = MD5.Create())
			{
				byte[] inputBytes = Encoding.UTF8.GetBytes(input);
				byte[] hashBytes = algorithm.ComputeHash(inputBytes);

				// Step 2, convert byte array to hex string.
				StringBuilder sb = new StringBuilder();
				foreach(byte b in hashBytes)
				{
					sb.Append(b.ToString("X2"));
				}

				return sb.ToString();
			}
		}
	}
}
