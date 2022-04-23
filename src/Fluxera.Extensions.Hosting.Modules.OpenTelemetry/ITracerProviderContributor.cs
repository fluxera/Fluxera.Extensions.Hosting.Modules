namespace Fluxera.Extensions.Hosting.Modules.OpenTelemetry
{
	using global::OpenTelemetry.Trace;
	using JetBrains.Annotations;

	/// <summary>
	///     A contract for contributors that configure OpenTelemetry tracing.
	/// </summary>
	[PublicAPI]
	public interface ITracerProviderContributor
	{
		/// <summary>
		///     Configure the OpenTelemetry tracing.
		/// </summary>
		/// <param name="builder"></param>
		void Configure(TracerProviderBuilder builder);
	}
}
