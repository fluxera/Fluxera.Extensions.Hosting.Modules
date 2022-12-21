namespace Fluxera.Extensions.Hosting.Modules.AspNetCore.HealthChecks
{
	using System;
	using System.Collections.Generic;
	using Fluxera.Extensions.DependencyInjection;
	using Fluxera.Extensions.Hosting.Modules.AspNetCore.HealthChecks.Contributors;
	using JetBrains.Annotations;
	using Microsoft.Extensions.DependencyInjection;
	using Microsoft.Extensions.Diagnostics.HealthChecks;

	/// <summary>
	///     A module that enabled the health checks.
	/// </summary>
	[PublicAPI]
	[DependsOn(typeof(AspNetCoreModule))]
	public sealed class HealthChecksModule : ConfigureServicesModule
	{
		/// <inheritdoc />
		public override void PreConfigureServices(IServiceConfigurationContext context)
		{
			// Add health check services.
			IHealthChecksBuilder healthChecksBuilder = context.Log("AddHealthChecks", services => services.AddHealthChecks());

			// Add health checks builder as singleton.
			context.Log("AddObjectAccessor(HealthChecksBuilder)", services =>
			{
				services.AddObjectAccessor(new HealthChecksBuilderContainer(healthChecksBuilder), ObjectAccessorLifetime.ConfigureServices);
			});

			// Initialize the contributor list.
			context.Log("AddObjectAccessor(HealthCheckContributorList)", services =>
			{
				services.AddObjectAccessor(new HealthCheckContributorList(), ObjectAccessorLifetime.ConfigureServices);
			});
		}

		/// <inheritdoc />
		public override void ConfigureServices(IServiceConfigurationContext context)
		{
			// Add the health check route contributor.
			context.Services.AddEndpointRouteContributor<EndpointRouteContributor>();

			// Add the default health check contributor.
			context.Services.AddHealthCheckContributor<HealthChecksContributor>();
		}

		/// <inheritdoc />
		public override void PostConfigureServices(IServiceConfigurationContext context)
		{
			context.Log("AddHealthChecks", services =>
			{
				HealthChecksBuilderContainer container = services.GetObject<HealthChecksBuilderContainer>();
				HealthCheckContributorList healthCheckContributorList = context.Services.GetObject<HealthCheckContributorList>();

				foreach(IHealthChecksContributor contributor in healthCheckContributorList)
				{
					contributor.ConfigureHealthChecks(container.Builder, context);
				}
			});
		}
	}
}
