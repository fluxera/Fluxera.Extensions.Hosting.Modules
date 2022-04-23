namespace Fluxera.Extensions.Hosting.Modules.OpenTelemetry.Contributors
{
	using Fluxera.Extensions.Hosting.Modules.Configuration;

	internal sealed class ConfigureOptionsContributor : ConfigureOptionsContributorBase<OpenTelemetryOptions>
	{
		/// <inheritdoc />
		public override string SectionName => "OpenTelemetry";
	}
}
