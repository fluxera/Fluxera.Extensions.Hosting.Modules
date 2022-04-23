namespace Fluxera.Extensions.Hosting.Modules.AspNetCore.Authentication.JwtBearer
{
	using JetBrains.Annotations;

	/// <summary>
	///     A dictionary containing JWT Bearer authentication schemes.
	/// </summary>
	[PublicAPI]
	public sealed class JwtBearerAuthenticationSchemes : AuthenticationSchemes<JwtBearerAuthenticationSchemeOptions>
	{
	}
}
