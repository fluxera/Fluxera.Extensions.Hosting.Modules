namespace Fluxera.Extensions.Hosting.Modules.Principal
{
	using System.Security.Claims;
	using JetBrains.Annotations;

	/// <summary>
	///     An accessor that provides the current user.
	/// </summary>
	[PublicAPI]
	public interface IPrincipalAccessor
	{
		/// <summary>
		///     Gets the current user.
		/// </summary>
		ClaimsPrincipal User { get; }
	}
}
