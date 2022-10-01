namespace Fluxera.Extensions.Hosting.Modules.AspNetCore.HealthChecks.Contributors
{
	internal sealed class HealthCheckContributor : IHealthCheckContributor
	{
		/// <inheritdoc />
		public HealthCheckDescriptor CreateHealthCheck(IServiceConfigurationContext context)
		{
			return new HealthCheckDescriptor(typeof(DefaultHealthCheck), "Default", HealthCheckCategory.Healthy);
		}
	}
}
