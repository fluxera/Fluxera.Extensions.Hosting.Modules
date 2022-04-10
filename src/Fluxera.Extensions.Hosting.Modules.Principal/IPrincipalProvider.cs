namespace Fluxera.Extensions.Hosting.Modules.Principal
{
	using System.Security.Claims;
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
	}
}
