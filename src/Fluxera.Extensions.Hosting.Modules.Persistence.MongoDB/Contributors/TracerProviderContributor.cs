namespace Fluxera.Extensions.Hosting.Modules.Persistence.MongoDB.Contributors
{
	using Fluxera.Extensions.Hosting.Modules.OpenTelemetry;
	using global::OpenTelemetry.Trace;

	internal sealed class TracerProviderContributor : ITracerProviderContributor
	{
		/// <inheritdoc />
		public void Configure(TracerProviderBuilder builder, IServiceConfigurationContext context)
		{
			// https://github.com/jbogard/MongoDB.Driver.Core.Extensions.OpenTelemetry
			builder.AddMongoDBInstrumentation();
		}
	}
}
