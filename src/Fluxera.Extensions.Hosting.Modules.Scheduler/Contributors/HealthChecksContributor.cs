namespace Fluxera.Extensions.Hosting.Modules.Scheduler.Contributors
{
	using Fluxera.Extensions.Hosting.Modules.HealthChecks;
	using Microsoft.Extensions.DependencyInjection;

	internal sealed class HealthChecksContributor : IHealthChecksContributor
	{
		/// <inheritdoc />
		public void ConfigureHealthChecks(IHealthChecksBuilder builder, IServiceConfigurationContext context)
		{
			//builder.AddCheck<QuartzHealthCheck>(name: "Quartz", tags: new string[] { HealthCheckTags.Ready });
		}
	}
}
