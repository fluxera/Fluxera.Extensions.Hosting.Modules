namespace Fluxera.Extensions.Hosting.Modules.AspNetCore.Authentication.JwtBearer
{
	using JetBrains.Annotations;

	/// <summary>
	///     The options for the JWt Bearer authentication module.
	/// </summary>
	[PublicAPI]
	public sealed class JwtBearerAuthenticationOptions
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
