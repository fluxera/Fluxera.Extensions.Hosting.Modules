namespace Fluxera.Extensions.Hosting.Modules.AspNetCore.HttpApi.Versioning.Contributors
{
	using Fluxera.Extensions.Hosting.Modules.Configuration;

	internal sealed class ConfigureOptionsContributor : ConfigureOptionsContributorBase<VersioningOptions>
	{
		/// <inheritdoc />
		public override string SectionName => "AspNetCore:HttpApi";
	}
}
