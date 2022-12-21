namespace Fluxera.Extensions.Hosting.Modules.AspNetCore.HealthChecks.Contributors
{
	using Fluxera.Utilities.Extensions;
	using JetBrains.Annotations;
	using Microsoft.AspNetCore.Builder;
	using Microsoft.AspNetCore.Diagnostics.HealthChecks;
	using Microsoft.AspNetCore.Routing;
	using Microsoft.Extensions.DependencyInjection;
	using Microsoft.Extensions.Options;

	[UsedImplicitly]
	internal sealed class EndpointRouteContributor : IEndpointRouteContributor
	{
		/// <inheritdoc />
		public void MapRoute(IEndpointRouteBuilder routeBuilder, IApplicationInitializationContext context)
		{
			HealthChecksOptions options = context.ServiceProvider.GetRequiredService<IOptions<HealthChecksOptions>>().Value;

			string livenessEndpointUrl = options.LivenessEndpointUrl.EnsureStartsWith("/");
			string readinessEndpointUrl = options.ReadinessEndpointUrl.EnsureStartsWith("/");
			string startupEndpointUrl = options.StartupEndpointUrl.EnsureStartsWith("/");

			context.Log($"MapHealthChecks({livenessEndpointUrl})", _ =>
			{
				routeBuilder.MapHealthChecks(livenessEndpointUrl, new HealthCheckOptions
				{
					Predicate = x => x.Tags.Contains("health"),
					AllowCachingResponses = false,
					ResponseWriter = HealthCheckResponseWriter.WriteHealthCheckResponse
				});
			});

			context.Log($"MapHealthChecks({readinessEndpointUrl})", _ =>
			{
				routeBuilder.MapHealthChecks(readinessEndpointUrl, new HealthCheckOptions
				{
					Predicate = x => x.Tags.Contains("ready"),
					AllowCachingResponses = false,
					ResponseWriter = HealthCheckResponseWriter.WriteHealthCheckResponse
				});
			});

			context.Log($"MapHealthChecks({startupEndpointUrl})", _ =>
			{
				routeBuilder.MapHealthChecks(startupEndpointUrl, new HealthCheckOptions
				{
					Predicate = x => x.Tags.Contains("startup") || x.Tags.Contains("ready"),
					AllowCachingResponses = false,
					ResponseWriter = HealthCheckResponseWriter.WriteHealthCheckResponse
				});
			});
		}
	}
}
