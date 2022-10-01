namespace Fluxera.Extensions.Hosting.Modules.AspNetCore.Warmup.Contributors
{
	using Fluxera.Extensions.Hosting.Modules.AspNetCore.HealthChecks;

	internal sealed class HealthCheckContributor : IHealthCheckContributor
	{
		/// <inheritdoc />
		public HealthCheckDescriptor CreateHealthCheck(IServiceConfigurationContext context)
		{
			return new HealthCheckDescriptor(typeof(WarmupHealthCheck), "Warmup", HealthCheckCategory.Ready);
		}
	}
}
