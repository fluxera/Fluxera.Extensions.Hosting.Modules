namespace Fluxera.Extensions.Hosting.Modules.AspNetCore.HealthChecks.Contributors
{
	using Fluxera.Extensions.Hosting.Modules.HealthChecks;
	using global::HealthChecks.ApplicationStatus.DependencyInjection;
	using Microsoft.Extensions.DependencyInjection;

	internal sealed class HealthChecksContributor : IHealthChecksContributor
	{
		/// <inheritdoc />
		public void ConfigureHealthChecks(IHealthChecksBuilder builder, IServiceConfigurationContext context)
		{
			builder.AddApplicationStatus("ApplicationStatus", tags: new string[]
			{
				HealthCheckTags.Ready
			});
		}
	}
}
