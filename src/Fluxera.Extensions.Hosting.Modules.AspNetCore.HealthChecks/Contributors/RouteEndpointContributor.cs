namespace Fluxera.Extensions.Hosting.Modules.AspNetCore.HealthChecks.Contributors
{
	using System.Linq;
	using JetBrains.Annotations;
	using Microsoft.AspNetCore.Builder;
	using Microsoft.AspNetCore.Diagnostics.HealthChecks;
	using Microsoft.AspNetCore.Routing;
	using Microsoft.Extensions.DependencyInjection;
	using Microsoft.Extensions.Logging;

	[UsedImplicitly]
	internal sealed class RouteEndpointContributor : IRouteEndpointContributor
	{
		public void MapRoute(IEndpointRouteBuilder routeBuilder)
		{
			ILogger<RouteEndpointContributor> logger = routeBuilder.ServiceProvider.GetRequiredService<ILogger<RouteEndpointContributor>>();

			logger.LogDebug("UseEndpoints -> MapHealthChecks(/healthz)");
			routeBuilder.MapHealthChecks("/healthz", new HealthCheckOptions
			{
				Predicate = x => x.Tags.Any(tag => tag == HealthCheckCategory.Healthy.ToString("G")),
				AllowCachingResponses = false,
				ResponseWriter = HealthCheckResponseWriter.WriteHealthCheckResponse
			});

			logger.LogDebug("UseEndpoints -> MapHealthChecks(/readyz)");
			routeBuilder.MapHealthChecks("/readyz", new HealthCheckOptions
			{
				Predicate = x => x.Tags.Any(tag => tag == HealthCheckCategory.Ready.ToString("G")),
				AllowCachingResponses = false,
				ResponseWriter = HealthCheckResponseWriter.WriteHealthCheckResponse
			});
		}
	}
}
