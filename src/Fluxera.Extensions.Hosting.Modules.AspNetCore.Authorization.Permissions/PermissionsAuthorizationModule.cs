namespace Fluxera.Extensions.Hosting.Modules.AspNetCore.Authorization.Permissions
{
	using JetBrains.Annotations;
	using MadEyeMatt.AspNetCore.Authorization.Permissions;

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
