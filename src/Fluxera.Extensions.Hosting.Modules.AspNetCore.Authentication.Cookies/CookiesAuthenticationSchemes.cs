namespace Fluxera.Extensions.Hosting.Modules.AspNetCore.Authentication.Cookies
{
	using JetBrains.Annotations;

	/// <summary>
	///     A dictionary containing Cookies authentication schemes.
	/// </summary>
	[PublicAPI]
	public sealed class CookiesAuthenticationSchemes : AuthenticationSchemes<CookiesAuthenticationSchemeOptions>
	{
	}
}
