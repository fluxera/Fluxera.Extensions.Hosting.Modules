namespace Fluxera.Extensions.Hosting.Modules.OpenTelemetry.Contributors
{
	using global::OpenTelemetry.Metrics;

	internal sealed class MeterProviderContributor : IMeterProviderContributor
	{
		/// <inheritdoc />
		public void Configure(MeterProviderBuilder builder, IServiceConfigurationContext context)
		{
			builder.AddRuntimeMetrics(options =>
			{
				// Note: All enabled
				//options.AssembliesEnabled = true;
				//options.GcEnabled = true;
				//options.JitEnabled = true;
				//options.ProcessEnabled = true;
				//options.ThreadingEnabled = true;
			});
		}
	}
}
