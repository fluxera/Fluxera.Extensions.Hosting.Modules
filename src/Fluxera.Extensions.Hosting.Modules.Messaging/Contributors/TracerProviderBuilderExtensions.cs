namespace Fluxera.Extensions.Hosting.Modules.Messaging.Contributors
{
	using global::OpenTelemetry.Trace;

	// https://github.com/open-telemetry/opentelemetry-dotnet-contrib/issues/326
	internal static class TracerProviderBuilderExtensions
	{
		public static void AddMassTransitInstrumentation(this TracerProviderBuilder builder)
		{
			builder.AddSource("MassTransit");
		}
	}
}
