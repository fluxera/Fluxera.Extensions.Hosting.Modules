namespace Fluxera.Extensions.Hosting.Modules.AspNetCore.HealthChecks.Contributors
{
	using Fluxera.Extensions.Hosting.Modules.Configuration;

	internal sealed class ConfigureOptionsContributor : ConfigureOptionsContributorBase<HealthChecksOptions>
	{
		/// <inheritdoc />
		public override string SectionName => "AspNetCore:HealthChecks";
	}
}
