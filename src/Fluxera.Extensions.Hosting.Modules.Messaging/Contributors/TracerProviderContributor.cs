namespace Fluxera.Extensions.Hosting.Modules.Messaging.Contributors
{
	using Fluxera.Extensions.Hosting.Modules.OpenTelemetry;
	using global::OpenTelemetry.Trace;

	internal sealed class TracerProviderContributor : ITracerProviderContributor
	{
		/// <inheritdoc />
		public void Configure(TracerProviderBuilder builder)
		{
			builder.AddMassTransitInstrumentation();
		}
	}
}
