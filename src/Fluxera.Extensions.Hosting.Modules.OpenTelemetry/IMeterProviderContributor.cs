namespace Fluxera.Extensions.Hosting.Modules.OpenTelemetry
{
	using global::OpenTelemetry.Metrics;
	using JetBrains.Annotations;

	/// <summary>
	///     A contract for contributors that configure OpenTelemetry metering.
	/// </summary>
	[PublicAPI]
	public interface IMeterProviderContributor
	{
		/// <summary>
		///     Configure the OpenTelemetry metering.
		/// </summary>
		/// <param name="builder"></param>
		/// <param name="context"></param>
		void Configure(MeterProviderBuilder builder, IServiceConfigurationContext context);
	}
}
