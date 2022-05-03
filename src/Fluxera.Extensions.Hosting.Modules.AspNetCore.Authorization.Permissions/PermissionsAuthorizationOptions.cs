namespace Fluxera.Extensions.Hosting.Modules.AspNetCore.Authorization.Permissions
{
	using JetBrains.Annotations;
	using Microsoft.AspNetCore.Identity;

	/// <summary>
	///     The options for the permission-based authorization module.
	/// </summary>
	[PublicAPI]
	public sealed class PermissionsAuthorizationOptions
	{
		/// <summary>
		///     Creates a new instance of the <see cref="PermissionsAuthorizationOptions" /> tyep.
		/// </summary>
		public PermissionsAuthorizationOptions()
		{
			this.Identity = new IdentityOptions();
		}

		/// <summary>
		///     Gets ot sets the Identity options.
		/// </summary>
		public IdentityOptions Identity { get; set; }
	}
}
