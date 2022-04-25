namespace Fluxera.Extensions.Hosting.Modules.AspNetCore.Authentication.Basic.Contributors
{
	using Fluxera.Extensions.Hosting.Modules.Configuration;

	internal sealed class ConfigureOptionsContributor : ConfigureOptionsContributorBase<BasicAuthenticationOptions>
	{
		/// <inheritdoc />
		public override string SectionName => "AspNetCore:Authentication:Schemes";
	}
}
