namespace Fluxera.Extensions.Hosting.Modules.AspNetCore.HealthChecks.Contributors
{
	using global::HealthChecks.ApplicationStatus.DependencyInjection;
	using Microsoft.Extensions.DependencyInjection;

	internal sealed class HealthChecksContributor : IHealthChecksContributor
	{
		/// <inheritdoc />
		public void ConfigureHealthChecks(IHealthChecksBuilder builder, IServiceConfigurationContext context)
		{
			builder.AddCheck<DefaultHealthCheck>("Default", tags: new string[]
			{
				HealthCheckTags.Health
			});

			builder.AddApplicationStatus("ApplicationStatus", tags: new string[]
			{
				HealthCheckTags.Ready
			});
		}
	}
}
