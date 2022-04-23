namespace Fluxera.Extensions.Hosting.Modules.AspNetCore.Authentication.JwtBearer
{
	using JetBrains.Annotations;

	/// <summary>
	///     The options for a JWT Bearer authentication scheme.
	/// </summary>
	[PublicAPI]
	public sealed class JwtBearerAuthenticationSchemeOptions
	{
		/// <summary>
		///     Gets ot sets the authority to use.
		/// </summary>
		public string Authority { get; set; }

		/// <summary>
		///     Gets or sets the signing key to use.
		/// </summary>
		public string SigningKey { get; set; }
	}
}
