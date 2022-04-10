namespace Fluxera.Extensions.Hosting.Modules.Localization.Contributors
{
	using Fluxera.Extensions.Hosting.Modules.Configuration;

	internal sealed class ConfigureOptionsContributor : ConfigureOptionsContributorBase<LocalizationOptions>
	{
		/// <inheritdoc />
		public override string SectionName => "Localization";
	}
}
