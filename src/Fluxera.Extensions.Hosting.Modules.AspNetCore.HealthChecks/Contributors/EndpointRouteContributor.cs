namespace Fluxera.Extensions.Hosting.Modules.AspNetCore.HealthChecks.Contributors
{
	using JetBrains.Annotations;
	using Microsoft.AspNetCore.Builder;
	using Microsoft.AspNetCore.Diagnostics.HealthChecks;
	using Microsoft.AspNetCore.Routing;

	[UsedImplicitly]
	internal sealed class EndpointRouteContributor : IEndpointRouteContributor
	{
		/// <inheritdoc />
		public void MapRoute(IEndpointRouteBuilder routeBuilder, IApplicationInitializationContext context)
		{
			context.Log("MapHealthChecks(/healthz)", _ =>
			{
				routeBuilder.MapHealthChecks("/healthz", new HealthCheckOptions
				{
					Predicate = x => x.Tags.Contains("health"),
					AllowCachingResponses = false,
					ResponseWriter = HealthCheckResponseWriter.WriteHealthCheckResponse
				});
			});

			context.Log("MapHealthChecks(/readyz)", _ =>
			{
				routeBuilder.MapHealthChecks("/readyz", new HealthCheckOptions
				{
					Predicate = x => x.Tags.Contains("ready"),
					AllowCachingResponses = false,
					ResponseWriter = HealthCheckResponseWriter.WriteHealthCheckResponse
				});
			});

			context.Log("MapHealthChecks(/startupz)", _ =>
			{
				routeBuilder.MapHealthChecks("/startupz", new HealthCheckOptions
				{
					Predicate = x => x.Tags.Contains("startup") || x.Tags.Contains("ready"),
					AllowCachingResponses = false,
					ResponseWriter = HealthCheckResponseWriter.WriteHealthCheckResponse
				});
			});
		}
	}
}
