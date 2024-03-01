namespace Fluxera.Extensions.Hosting.Modules.Scheduler.Contributors
{
	using Fluxera.Extensions.Hosting.Modules.OpenTelemetry;
	using global::OpenTelemetry.Trace;

	internal sealed class TracerProviderContributor : ITracerProviderContributor
	{
		/// <inheritdoc />
		public void Configure(TracerProviderBuilder builder, IServiceConfigurationContext context)
		{
			// TODO
			//builder.AddQuartzInstrumentation();
		}
	}
}
