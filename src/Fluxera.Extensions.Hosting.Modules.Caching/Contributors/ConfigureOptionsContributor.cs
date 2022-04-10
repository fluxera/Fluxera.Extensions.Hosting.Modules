namespace Fluxera.Extensions.Hosting.Modules.Caching.Contributors
{
	using Fluxera.Extensions.Hosting.Modules.Configuration;

	internal sealed class ConfigureOptionsContributor : ConfigureOptionsContributorBase<CachingOptions>
	{
		/// <inheritdoc />
		public override string SectionName => "Caching";
	}
}
