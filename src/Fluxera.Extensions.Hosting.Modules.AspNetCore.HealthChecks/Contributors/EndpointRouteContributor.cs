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
		public void MapRoute(IEndpointRouteBuilder endpoints, IApplicationInitializationContext context)
		{
			HealthChecksOptions options = context.ServiceProvider.GetRequiredService<IOptions<HealthChecksOptions>>().Value;

			string livenessEndpointUrl = options.LivenessEndpointUrl.EnsureStartsWith("/");
			string readinessEndpointUrl = options.ReadinessEndpointUrl.EnsureStartsWith("/");
			string startupEndpointUrl = options.StartupEndpointUrl.EnsureStartsWith("/");

			if(!string.IsNullOrWhiteSpace(options.EndpointUrlPrefix))
			{
				endpoints = endpoints.MapGroup(options.EndpointUrlPrefix);
			}

			context.Log($"MapHealthChecks({livenessEndpointUrl})", _ =>
			{
				endpoints.MapHealthChecks(livenessEndpointUrl, new HealthCheckOptions
				{
					Predicate = x => x.Tags.Contains("health"),
					AllowCachingResponses = false,
					ResponseWriter = HealthCheckResponseWriter.WriteHealthCheckResponse
				}).AllowAnonymous();
			});

			context.Log($"MapHealthChecks({readinessEndpointUrl})", _ =>
			{
				endpoints.MapHealthChecks(readinessEndpointUrl, new HealthCheckOptions
				{
					Predicate = x => x.Tags.Contains("ready"),
					AllowCachingResponses = false,
					ResponseWriter = HealthCheckResponseWriter.WriteHealthCheckResponse
				}).AllowAnonymous();
			});

			context.Log($"MapHealthChecks({startupEndpointUrl})", _ =>
			{
				endpoints.MapHealthChecks(startupEndpointUrl, new HealthCheckOptions
				{
					Predicate = x => x.Tags.Contains("startup") || x.Tags.Contains("ready"),
					AllowCachingResponses = false,
					ResponseWriter = HealthCheckResponseWriter.WriteHealthCheckResponse
				}).AllowAnonymous();
			});
		}
	}
}
