namespace Fluxera.Extensions.Hosting.Modules.AspNetCore.Warmup
{
	using System.Collections.Generic;
	using Fluxera.Extensions.DependencyInjection;
	using Fluxera.Extensions.Hosting.Modules.AspNetCore.HealthChecks;
	using Fluxera.Extensions.Hosting.Modules.AspNetCore.Warmup.Contributors;
	using Fluxera.Extensions.Hosting.Modules.HealthChecks;
	using JetBrains.Annotations;
	using Microsoft.Extensions.DependencyInjection;
	using Microsoft.Extensions.DependencyInjection.Extensions;

	/// <summary>
	///     A module that enabled service warmup.
	/// </summary>
	[PublicAPI]
	[DependsOn(typeof(HealthChecksEndpointsModule))]
	[DependsOn(typeof(AspNetCoreModule))]
	public sealed class WarmupModule : ConfigureServicesModule
	{
		/// <inheritdoc />
		public override void PreConfigureServices(IServiceConfigurationContext context)
		{
			// Add the service warmup services.
			context.Log("AddEndpointInit", services => services.TryAddSingleton(context.Services));

			// Initialize the contributor list.
			context.Log("AddObjectAccessor(WarmupContributorList)", services =>
			{
				services.AddObjectAccessor(new WarmupContributorList(), ObjectAccessorLifetime.ConfigureServices);
			});
		}

		/// <inheritdoc />
		public override void ConfigureServices(IServiceConfigurationContext context)
		{
			// Add the warmup contributor.
			context.Services.AddWarmupContributor<WarmupContributor>();

			// Add the readiness health check contributor.
			context.Services.AddHealthCheckContributor<HealthChecksContributor>();
		}

		/// <inheritdoc />
		public override void PostConfigureServices(IServiceConfigurationContext context)
		{
			context.Log("AddWarmupRoutines", services =>
			{
				WarmupContributorList contributorList = context.Services.GetObject<WarmupContributorList>();

				foreach(IWarmupContributor contributor in contributorList)
				{
					IEnumerable<EndpointInitDescriptor> descriptors = contributor.CreateEndpointInit(context);

					foreach(EndpointInitDescriptor descriptor in descriptors)
					{
						services.AddSingleton(typeof(IEndpointInit), descriptor.Type);
					}
				}
			});
		}
	}
}
