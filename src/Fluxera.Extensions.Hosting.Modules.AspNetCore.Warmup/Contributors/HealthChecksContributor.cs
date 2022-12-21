namespace Fluxera.Extensions.Hosting.Modules.AspNetCore.Warmup.Contributors
{
	using Fluxera.Extensions.Hosting.Modules.AspNetCore.HealthChecks;
	using Microsoft.Extensions.DependencyInjection;

	internal sealed class HealthChecksContributor : IHealthChecksContributor
	{
		/// <inheritdoc />
		public void ConfigureHealthChecks(IHealthChecksBuilder builder, IServiceConfigurationContext context)
		{
			string[] tags = { HealthCheckTags.Startup };
			builder.AddCheck<WarmupHealthCheck>("Warmup", tags: tags);
		}
	}
}
