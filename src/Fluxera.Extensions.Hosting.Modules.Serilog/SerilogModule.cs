namespace Fluxera.Extensions.Hosting.Modules.Serilog
{
	using Fluxera.Extensions.Hosting.Modules.OpenTelemetry;
	using JetBrains.Annotations;

	/// <summary>
	///     A module that enables Serilog logging.
	/// </summary>
	[PublicAPI]
	[DependsOn<OpenTelemetryModule>]
	public sealed class SerilogModule : IModule
	{
	}
}
