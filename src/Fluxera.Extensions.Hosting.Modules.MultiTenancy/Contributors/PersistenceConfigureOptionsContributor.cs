namespace Fluxera.Extensions.Hosting.Modules.MultiTenancy.Contributors
{
	using Fluxera.Extensions.Hosting.Modules.Configuration;

	internal sealed class PersistenceConfigureOptionsContributor : ConfigureOptionsContributorBase<MultiTenancyPersistenceOptions>
	{
		/// <inheritdoc />
		public override string SectionName => "Persistence";
	}
}
