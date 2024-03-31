namespace Fluxera.Extensions.Hosting.Modules.AspNetCore.Contributors
{
	using Fluxera.Extensions.Hosting.Modules.Configuration;

	internal sealed class ConfigureOptionsContributor : ConfigureOptionsContributorBase<AspNetCoreOptions>
	{
		/// <inheritdoc />
		public override string SectionName => "App";
	}
}
