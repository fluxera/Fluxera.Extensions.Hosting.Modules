namespace Fluxera.Extensions.Hosting.Modules.AspNetCore.OpenTelemetry
{
	using Fluxera.Extensions.Hosting.Modules.OpenTelemetry;
	using JetBrains.Annotations;

	/// <summary>
	///     A module that enables OpenTelemetry monitoring for ASP.NET Core.
	/// </summary>
	[PublicAPI]
	[DependsOn(typeof(OpenTelemetryModule))]
	[DependsOn(typeof(AspNetCoreModule))]
	public sealed class AspNetCoreOpenTelemetryModule : ConfigureServicesModule
	{
		/// <inheritdoc />
		public override void ConfigureServices(IServiceConfigurationContext context)
		{
		}
	}
}
