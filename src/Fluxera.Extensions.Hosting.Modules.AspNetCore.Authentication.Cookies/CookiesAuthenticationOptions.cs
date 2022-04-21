namespace Fluxera.Extensions.Hosting.Modules.AspNetCore.Authentication.Cookies
{
	using JetBrains.Annotations;

	/// <summary>
	///     The options for the Basic authentication module.
	/// </summary>
	[PublicAPI]
	public sealed class CookiesAuthenticationOptions
	{
		/// <summary>
		///     Creates a new instance of the <see cref="CookiesAuthenticationOptions" /> type.
		/// </summary>
		public CookiesAuthenticationOptions()
		{
			this.Cookies = new CookiesAuthenticationSchemes();
		}

		/// <summary>
		///     Gets or sets the Basic authentication schemes.
		/// </summary>
		public CookiesAuthenticationSchemes Cookies { get; set; }
	}
}
