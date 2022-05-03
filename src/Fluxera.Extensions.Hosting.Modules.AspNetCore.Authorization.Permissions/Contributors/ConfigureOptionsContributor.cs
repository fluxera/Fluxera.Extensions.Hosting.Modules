namespace Fluxera.Extensions.Hosting.Modules.AspNetCore.Authorization.Permissions.Contributors
{
	using Fluxera.Extensions.Hosting.Modules.Configuration;

	internal sealed class ConfigureOptionsContributor : ConfigureOptionsContributorBase<PermissionsAuthorizationOptions>
	{
		/// <inheritdoc />
		public override string SectionName => "AspNetCore:Authorization";
	}
}
