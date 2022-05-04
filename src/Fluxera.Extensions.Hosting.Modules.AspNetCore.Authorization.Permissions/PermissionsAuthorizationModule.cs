namespace Fluxera.Extensions.Hosting.Modules.AspNetCore.Authorization.Permissions
{
	using global::AspNetCore.Authorization.Permissions;
	using JetBrains.Annotations;

	/// <summary>
	///     A module that enabled permission-based authorization.
	/// </summary>
	[PublicAPI]
	[DependsOn(typeof(AuthorizationModule))]
	public sealed class PermissionsAuthorizationModule : ConfigureServicesModule
	{
		/// <inheritdoc />
		public override void ConfigureServices(IServiceConfigurationContext context)
		{
			// Add the permission-based authorization policy.
			context.Log("AddPermissionsAuthorization",
				services => services.AddPermissionsAuthorization());
		}
	}
}
