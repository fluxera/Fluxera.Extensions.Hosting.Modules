namespace Fluxera.Extensions.Hosting.Modules.AspNetCore.OData.Contributors
{
	using Fluxera.Extensions.Hosting.Modules.Configuration;

	internal sealed class ConfigureOptionsContributor : ConfigureOptionsContributorBase<OData.ODataOptions>
	{
		/// <inheritdoc />
		public override string SectionName => "AspNetCore.HttpApi";
	}
}
