namespace Fluxera.Extensions.Hosting.Modules.AspNetCore.HealthChecks
{
	using JetBrains.Annotations;

	/// <summary>
	///     A contract for contributors that provide health checks.
	/// </summary>
	[PublicAPI]
	public interface IHealthCheckContributor
	{
		/// <summary>
		///     Creates health check descriptor. The health check is added to the existing
		///     health checks using the data from the descriptor.
		/// </summary>
		/// <returns></returns>
		HealthCheckDescriptor CreateHealthCheck();
	}
}
