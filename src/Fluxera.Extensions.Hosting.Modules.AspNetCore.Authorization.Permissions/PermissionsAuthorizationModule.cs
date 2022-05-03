namespace Fluxera.Extensions.Hosting.Modules.AspNetCore.Authorization.Permissions
{
	using Fluxera.Extensions.Hosting.Modules.AspNetCore.Authorization.Permissions.Contributors;
	using Fluxera.Extensions.Hosting.Modules.Configuration;
	using global::AspNetCore.Authorization.Permissions;
	using global::AspNetCore.Authorization.Permissions.Identity;
	using JetBrains.Annotations;

	/// <summary>
	///     A module that enabled permission-based authorization.
	/// </summary>
	[PublicAPI]
	[DependsOn(typeof(AuthorizationModule))]
	public sealed class PermissionsAuthorizationModule : ConfigureServicesModule
	{
		/// <inheritdoc />
		public override void PreConfigureServices(IServiceConfigurationContext context)
		{
			// Add the configure options contributor.
			context.Services.AddConfigureOptionsContributor<ConfigureOptionsContributor>();
		}

		/// <inheritdoc />
		public override void ConfigureServices(IServiceConfigurationContext context)
		{
			// Add the permission-based authorization policy.
			context.Log("AddPermissionsAuthorization", services =>
			{
				services.AddPermissionsAuthorization(options =>
				{
					// TODO: Should we initialize the Identity system here? Or in a separate module?
					options.AddIdentityClaimsProvider();
				});
			});

			// TODO: Should we initialize the Identity system here? Or in a separate module?
			PermissionsIdentityBuilder identityBuilder = context.Log("AddPermissionsIdentity", services =>
			{
				PermissionsAuthorizationOptions permissionsOptions = services.GetOptions<PermissionsAuthorizationOptions>();

				return services.AddPermissionsIdentity(options =>
				{
					options.ClaimsIdentity = permissionsOptions.Identity.ClaimsIdentity;
				});
			});
		}
	}
}
