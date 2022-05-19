namespace Fluxera.Extensions.Hosting.Modules.Principal
{
	using System.Security.Claims;
	using System.Threading.Tasks;
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

		/// <summary>
		///     Gets the underlying access token of the principal.
		/// </summary>
		/// <returns></returns>
		Task<string> GetAccessTokenAsync();
	}
}
