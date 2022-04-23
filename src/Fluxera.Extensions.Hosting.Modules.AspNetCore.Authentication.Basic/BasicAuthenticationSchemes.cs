namespace Fluxera.Extensions.Hosting.Modules.AspNetCore.Authentication.Basic
{
	using JetBrains.Annotations;

	/// <summary>
	///     A dictionary containing Basic authentication schemes.
	/// </summary>
	[PublicAPI]
	public sealed class BasicAuthenticationSchemes : AuthenticationSchemes<BasicAuthenticationSchemeOptions>
	{
	}
}
