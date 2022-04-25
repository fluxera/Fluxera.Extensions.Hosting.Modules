namespace Fluxera.Extensions.Hosting.Modules.AspNetCore.HttpApi.Contributors
{
	using Fluxera.Extensions.Hosting.Modules.Configuration;

	internal sealed class ConfigureOptionsContributor : ConfigureOptionsContributorBase<HttpApiOptions>
	{
		/// <inheritdoc />
		public override string SectionName => "AspNetCore.HttpApi";
	}
}
