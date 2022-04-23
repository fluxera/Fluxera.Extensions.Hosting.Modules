namespace Fluxera.Extensions.Hosting.Modules.AspNetCore.Authentication.ApiKey
{
	using JetBrains.Annotations;

	/// <summary>
	///     A dictionary containing ApiKey authentication schemes.
	/// </summary>
	[PublicAPI]
	public sealed class ApiKeyAuthenticationSchemes : AuthenticationSchemes<ApiKeyAuthenticationSchemeOptions>
	{
	}
}
