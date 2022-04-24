namespace Fluxera.Extensions.Hosting.Modules.AspNetCore.Versioning.Contributors
{
	using Fluxera.Extensions.Hosting.Modules.Configuration;

	internal sealed class ConfigureOptionsContributor : ConfigureOptionsContributorBase<VersioningOptions>
	{
		/// <inheritdoc />
		public override string SectionName => "AspNetCore:Versioning";
	}
}
