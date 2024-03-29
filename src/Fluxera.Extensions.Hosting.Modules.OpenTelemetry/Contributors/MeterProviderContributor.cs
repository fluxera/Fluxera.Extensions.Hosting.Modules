﻿namespace Fluxera.Extensions.Hosting.Modules.OpenTelemetry.Contributors
{
	using global::OpenTelemetry.Metrics;

	internal sealed class MeterProviderContributor : IMeterProviderContributor
	{
		/// <inheritdoc />
		public void Configure(MeterProviderBuilder builder, IServiceConfigurationContext context)
		{
			builder.AddProcessInstrumentation();
			builder.AddRuntimeInstrumentation();
		}
	}
}
