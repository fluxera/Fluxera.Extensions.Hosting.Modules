namespace Fluxera.Extensions.Hosting.Modules.AspNetCore.Authentication.ApiKey.Contributors
{
	using Fluxera.Extensions.Hosting.Modules.Configuration;

	internal sealed class ConfigureOptionsContributor : ConfigureOptionsContributorBase<ApiKeyAuthenticationOptions>
	{
		/// <inheritdoc />
		public override string SectionName => "Authentication:ApiKey";
	}
}
