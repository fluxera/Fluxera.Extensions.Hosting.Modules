namespace Fluxera.Extensions.Hosting.Modules.AspNetCore.Authentication.ApiKey
{
	using JetBrains.Annotations;

	/// <summary>
	///     The scheme modes of the ApiKey authentication.
	/// </summary>
	[PublicAPI]
	public enum ApiKeySchemeMode
	{
		/// <summary>
		///     API Key - In Header authentication scheme.
		/// </summary>
		KeyInHeader,

		/// <summary>
		///     API Key - In Authorization Header authentication scheme.
		/// </summary>
		ApiKeyInAuthorizationHeader,

		/// <summary>
		///     API Key - In Query Parameters authentication scheme.
		/// </summary>
		ApiKeyInQueryParams,

		/// <summary>
		///     API Key - In Header Or Query Parameters authentication scheme.
		/// </summary>
		ApiKeyInHeaderOrQueryParams
	}
}
