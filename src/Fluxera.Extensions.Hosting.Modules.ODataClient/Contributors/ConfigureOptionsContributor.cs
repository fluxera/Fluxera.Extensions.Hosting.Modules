namespace Fluxera.Extensions.Hosting.Modules.ODataClient.Contributors
{
	using Fluxera.Extensions.Hosting.Modules.Configuration;

	internal sealed class ConfigureOptionsContributor : ConfigureOptionsContributorBase<ODataClientOptions>
	{
		/// <inheritdoc />
		public override string SectionName => "HttpClient";
	}
}
