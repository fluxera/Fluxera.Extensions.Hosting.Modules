namespace Fluxera.Extensions.Hosting.Modules.OpenTelemetry
{
	using JetBrains.Annotations;
	using Microsoft.Extensions.Configuration;

	/// <summary>
	///     The options for the OpenTelemetry module.
	/// </summary>
	[PublicAPI]
	public sealed class OpenTelemetryOptions
	{
		/// <summary>
		///     Gets or sets the endpoint for the OpenTelemetryProtocol.
		/// </summary>
		[ConfigurationKeyName("Endpoint")]
		public string OpenTelemetryProtocolEndpoint { get; set; } = "http://localhost:4317";
	}
}
