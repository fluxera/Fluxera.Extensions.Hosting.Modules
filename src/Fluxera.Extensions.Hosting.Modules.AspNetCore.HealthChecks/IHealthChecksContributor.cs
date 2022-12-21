namespace Fluxera.Extensions.Hosting.Modules.AspNetCore.HealthChecks
{
	using JetBrains.Annotations;
	using Microsoft.Extensions.DependencyInjection;

	/// <summary>
	///     A contract for contributors that provide health checks.
	/// </summary>
	[PublicAPI]
	public interface IHealthChecksContributor
	{
		/// <summary>
		///     Creates health check descriptor. The health check is added to the existing
		///     health checks using the data from the descriptor.
		/// </summary>
		/// <param name="builder"></param>
		/// <param name="context"></param>
		/// <returns></returns>
		void ConfigureHealthChecks(IHealthChecksBuilder builder, IServiceConfigurationContext context);
	}
}
