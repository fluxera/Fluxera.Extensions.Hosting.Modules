namespace Fluxera.Extensions.Hosting.Modules.AspNetCore.HttpApi.OData.Contributors
{
	using Fluxera.Extensions.Hosting.Modules.Configuration;

	internal sealed class ConfigureOptionsContributor : ConfigureOptionsContributorBase<ODataOptions>
	{
		/// <inheritdoc />
		public override string SectionName => "HttpApi";
	}
}
