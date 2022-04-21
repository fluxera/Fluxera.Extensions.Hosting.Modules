namespace Fluxera.Extensions.Hosting.Modules.AspNetCore.Authentication.Cookies
{
	using System;
	using JetBrains.Annotations;
	using Microsoft.AspNetCore.Http;

	/// <summary>
	///     The options for a Cookies authentication scheme.
	/// </summary>
	[PublicAPI]
	public sealed class CookiesAuthenticationSchemeOptions
	{
		/// <summary>
		///     Creates a new instance of the <see cref="CookiesAuthenticationSchemeOptions" /> type.
		/// </summary>
		public CookiesAuthenticationSchemeOptions()
		{
			this.Cookie = new CookieOptions();
		}

		/// <summary>
		///     Gets or ses the cookie options.
		/// </summary>
		public CookieOptions Cookie { get; set; }

		/// <summary>
		///     The SlidingExpiration is set to true to instruct the handler to re-issue a new cookie with a new
		///     expiration time any time it processes a request which is more than halfway through the expiration window.
		/// </summary>
		public bool SlidingExpiration { get; set; } = true;

		/// <summary>
		///     The LoginPath property is used by the handler for the redirection target when handling ChallengeAsync.
		///     The current url which is added to the LoginPath as a query string parameter named by the ReturnUrlParameter.
		///     Once a request to the LoginPath grants a new SignIn identity, the ReturnUrlParameter value is used to redirect
		///     the browser back to the original url.
		/// </summary>
		public PathString LoginPath { get; set; }

		/// <summary>
		///     If the LogoutPath is provided the handler then a request to that path will redirect based on the
		///     ReturnUrlParameter.
		/// </summary>
		public PathString LogoutPath { get; set; }

		/// <summary>
		///     The AccessDeniedPath property is used by the handler for the redirection target when handling ForbidAsync.
		/// </summary>
		public PathString AccessDeniedPath { get; set; }

		/// <summary>
		///     <para>
		///         Controls how much time the authentication ticket stored in the cookie will remain valid from the point it is
		///         created
		///         The expiration information is stored in the protected cookie ticket. Because of that an expired cookie will be
		///         ignored
		///         even if it is passed to the server after the browser should have purged it.
		///     </para>
		///     <para>
		///         This is separate from the value of <see cref="CookieOptions.Expires" />, which specifies
		///         how long the browser will keep the cookie.
		///     </para>
		/// </summary>
		public TimeSpan ExpireTimeSpan { get; set; } = TimeSpan.FromDays(14);
	}
}
