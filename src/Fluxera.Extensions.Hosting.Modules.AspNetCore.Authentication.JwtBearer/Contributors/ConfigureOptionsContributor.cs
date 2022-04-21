namespace Fluxera.Extensions.Hosting.Modules.AspNetCore.Authentication.JwtBearer.Contributors
{
	using Fluxera.Extensions.Hosting.Modules.Configuration;

	internal sealed class ConfigureOptionsContributor : ConfigureOptionsContributorBase<JwtBearerAuthenticationOptions>
	{
		/// <inheritdoc />
		public override string SectionName => "Authentication:Schemes";
	}
}
