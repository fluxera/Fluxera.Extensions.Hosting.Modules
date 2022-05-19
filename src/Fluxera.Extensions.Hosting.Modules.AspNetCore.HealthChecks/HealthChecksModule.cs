namespace Fluxera.Extensions.Hosting.Modules.AspNetCore.HealthChecks
{
	using Fluxera.Extensions.DependencyInjection;
	using Fluxera.Extensions.Hosting.Modules.AspNetCore.HealthChecks.Contributors;
	using JetBrains.Annotations;
	using Microsoft.Extensions.DependencyInjection;

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
			// Add the health check route contributor.
			context.Services.AddEndpointRouteContributor<EndpointRouteContributor>();

			// Add health check services.
			IHealthChecksBuilder healthChecksBuilder = context.Log("AddHealthChecks", services =>
			{
				return services
					.AddHealthChecks()
					.AddCheck<DefaultHealthCheck>("Default", tags: new string[]
					{
						HealthCheckCategory.Healthy.ToString("G"),
					});
			});

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
		public override void PostConfigureServices(IServiceConfigurationContext context)
		{
			context.Log("AddHealthChecks", services =>
			{
				HealthChecksBuilderContainer container = services.GetObject<HealthChecksBuilderContainer>();
				HealthCheckContributorList healthCheckContributorList = context.Services.GetObject<HealthCheckContributorList>();

				foreach(IHealthCheckContributor contributor in healthCheckContributorList)
				{
					HealthCheckDescriptor descriptor = contributor.CreateHealthCheck(context);

					container.Builder.AddCheck(descriptor.Name, descriptor.Type, tags: new string[]
					{
						descriptor.Category.ToString("G")
					});
				}
			});
		}
	}
}
