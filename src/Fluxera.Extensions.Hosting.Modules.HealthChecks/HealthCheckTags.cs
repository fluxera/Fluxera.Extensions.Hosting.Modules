namespace Fluxera.Extensions.Hosting.Modules.HealthChecks
{
	using JetBrains.Annotations;

	/// <summary>
	///     The available health check tags.
	/// </summary>
	[PublicAPI]
	public static class HealthCheckTags
	{
		/// <summary>
		///     The checks in this category are executed when the 'healthz' endpoint is called.
		/// </summary>
		public const string Health = "health";

		/// <summary>
		///     The checks in this category are executed when the 'readyz' endpoint is called.
		/// </summary>
		public const string Ready = "ready";

		/// <summary>
		///     The checks in this category are executed when the 'startupz' endpoint is called.
		/// </summary>
		public const string Startup = "startup";
	}
}
