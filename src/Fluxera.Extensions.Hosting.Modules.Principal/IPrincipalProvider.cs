namespace Fluxera.Extensions.Hosting.Modules.Principal
{
	using System.Security.Claims;
	using System.Threading.Tasks;
	using JetBrains.Annotations;

	/// <summary>
	///     A contract for providers of the current user.
	/// </summary>
	[PublicAPI]
	public interface IPrincipalProvider
	{
		/// <summary>
		///     Gets the position where this contributor is sorted.
		/// </summary>
		int Position { get; }

		/// <summary>
		///     Gets the current user.
		/// </summary>
		ClaimsPrincipal User { get; }

		/// <summary>
		///     Gets the underlying access token (if any) for the current user.
		/// </summary>
		/// <returns></returns>
		Task<string> GetAccessTokenAsync();
	}
}
