namespace Fluxera.Extensions.Hosting.Modules.Persistence.EntityFrameworkCore.Contributors
{
	using Fluxera.Extensions.Hosting.Modules.OpenTelemetry;
	using global::OpenTelemetry.Trace;

	internal sealed class TracerProviderContributor : ITracerProviderContributor
	{
		/// <inheritdoc />
		public void Configure(TracerProviderBuilder builder, IServiceConfigurationContext context)
		{
			// https://github.com/open-telemetry/opentelemetry-dotnet-contrib
			builder.AddEntityFrameworkCoreInstrumentation();
		}
	}
}
