namespace Fluxera.Extensions.Hosting.Modules.Persistence.MongoDB.Contributors
{
	using global::OpenTelemetry.Trace;

	/// <summary>
	///     Provides extension methods to enable the trace instrumentation for the Repository.
	/// </summary>
	internal static class TracerProviderBuilderExtensions
	{
		/// <summary>
		///     Adds the diagnostic source for the MongoDB driver to OpenTelemetry.
		/// </summary>
		/// <param name="builder"></param>
		/// <returns></returns>
		public static TracerProviderBuilder AddMongoDBInstrumentation(this TracerProviderBuilder builder)
		{
			return builder.AddSource("MongoDB.Driver.Core.Extensions.DiagnosticSources");
		}
	}
}
