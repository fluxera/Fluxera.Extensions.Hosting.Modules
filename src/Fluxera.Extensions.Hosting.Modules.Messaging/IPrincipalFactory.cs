namespace Fluxera.Extensions.Hosting.Modules.Messaging
{
	using System.Security.Claims;
	using JetBrains.Annotations;

	/// <summary>
	///     Contract for a factory that produces claims principals from access tokens.
	/// </summary>
	[PublicAPI]
	public interface IPrincipalFactory
	{
		/// <summary>
		///     Creates a <see cref="ClaimsPrincipal" /> from the given JWT access-token.
		/// </summary>
		/// <param name="accessToken"></param>
		/// <returns></returns>
		ClaimsPrincipal CreatePrincipal(string accessToken);
	}
}
