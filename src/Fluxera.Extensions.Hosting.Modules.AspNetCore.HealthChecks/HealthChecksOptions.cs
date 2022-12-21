namespace Fluxera.Extensions.Hosting.Modules.AspNetCore.HealthChecks
{
	using JetBrains.Annotations;

	/// <summary>
	///		An options class for the health checks.
	/// </summary>
	[PublicAPI]
	public sealed class HealthChecksOptions
	{
		/// <summary>
		///		Gets or sets the endpoint URL of the liveness probe.
		/// </summary>
		public string LivenessEndpointUrl { get; set; } = "healthz";

		/// <summary>
		///		Gets or sets the endpoint URL of the readiness probe.
		/// </summary>
		public string ReadinessEndpointUrl { get; set; } = "readyz";

		/// <summary>
		///		Gets or sets the endpoint URL of the startup probe.
		/// </summary>
		public string StartupEndpointUrl { get; set; } = "startupz";
	}
}
