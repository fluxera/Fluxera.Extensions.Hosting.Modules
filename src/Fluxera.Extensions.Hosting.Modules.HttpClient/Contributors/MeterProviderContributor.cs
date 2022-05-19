namespace Fluxera.Extensions.Hosting.Modules.HttpClient.Contributors
{
	using Fluxera.Extensions.Hosting.Modules.OpenTelemetry;
	using global::OpenTelemetry.Metrics;

	internal sealed class MeterProviderContributor : IMeterProviderContributor
	{
		/// <inheritdoc />
		public void Configure(MeterProviderBuilder builder, IServiceConfigurationContext context)
		{
			builder.AddHttpClientInstrumentation();
		}
	}
}
