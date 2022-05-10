namespace Fluxera.Extensions.Hosting.Modules.AspNetCore.HealthChecks.Contributors
{
	using System.Linq;
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
					Predicate = x => x.Tags.Any(tag => tag == HealthCheckCategory.Healthy.ToString("G")),
					AllowCachingResponses = false,
					ResponseWriter = HealthCheckResponseWriter.WriteHealthCheckResponse
				});
			});

			context.Log("MapHealthChecks(/readyz)", _ =>
			{
				routeBuilder.MapHealthChecks("/readyz", new HealthCheckOptions
				{
					Predicate = x => x.Tags.Any(tag => tag == HealthCheckCategory.Ready.ToString("G")),
					AllowCachingResponses = false,
					ResponseWriter = HealthCheckResponseWriter.WriteHealthCheckResponse
				});
			});
		}
	}
}
