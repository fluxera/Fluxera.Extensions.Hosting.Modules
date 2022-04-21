namespace Fluxera.Extensions.Hosting.Modules.AspNetCore.Authentication.Basic
{
	using JetBrains.Annotations;

	/// <summary>
	///     The options for a Basic authentication scheme.
	/// </summary>
	[PublicAPI]
	public sealed class BasicAuthenticationSchemeOptions
	{
		/// <summary>
		///     Gets or sets the realm property. It is used with WWW-Authenticate response header when challenging un-authenticated
		///     requests.
		///     Required to be set if SuppressWWWAuthenticateHeader is not set to true.
		///     <see href="https://tools.ietf.org/html/rfc7235#section-2.2" />
		/// </summary>
		public string Realm { get; set; }

		/// <summary>
		///     Default value is false.
		///     When set to true, it will NOT return WWW-Authenticate response header when challenging un-authenticated requests.
		///     When set to false, it will return WWW-Authenticate response header when challenging un-authenticated requests.
		///     It is normally used to disable browser prompt when doing ajax calls.
		///     <see href="https://tools.ietf.org/html/rfc7235#section-4.1" />
		/// </summary>
		public bool SuppressWWWAuthenticateHeader { get; set; }

		/// <summary>
		///     Default value is false.
		///     If set to true, it checks if AllowAnonymous filter on controller action or metadata on the endpoint which, if
		///     found, it does not try to authenticate the request.
		/// </summary>
		public bool IgnoreAuthenticationIfAllowAnonymous { get; set; }
	}
}
