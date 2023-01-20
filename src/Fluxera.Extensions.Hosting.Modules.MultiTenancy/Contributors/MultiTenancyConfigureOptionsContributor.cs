namespace Fluxera.Extensions.Hosting.Modules.MultiTenancy.Contributors
{
	using Fluxera.Extensions.Hosting.Modules.Configuration;

	internal sealed class MultiTenancyConfigureOptionsContributor : ConfigureOptionsContributorBase<MultiTenancyOptions>
	{
		/// <inheritdoc />
		public override string SectionName => "MultiTenancy";
	}
}
