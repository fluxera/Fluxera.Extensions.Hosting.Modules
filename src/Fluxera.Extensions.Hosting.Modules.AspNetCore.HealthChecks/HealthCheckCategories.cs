namespace Fluxera.Extensions.Hosting.Modules.AspNetCore.HealthChecks
{
	using JetBrains.Annotations;

	/// <summary>
	///     The available health check categories.
	/// </summary>
	[PublicAPI]
	public enum HealthCheckCategory
	{
		/// <summary>
		///     The checks in this category are executed when the 'healthz' endpoint is called.
		/// </summary>
		Healthy,

		/// <summary>
		///     The checks in this category are executed when the 'readyz' endpoint is called.
		/// </summary>
		Ready
	}
}
