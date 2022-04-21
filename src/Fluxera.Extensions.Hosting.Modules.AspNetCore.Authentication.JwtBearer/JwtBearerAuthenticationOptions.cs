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
		///     Creates a new instance of the <see cref="JwtBearerAuthenticationOptions" /> type.
		/// </summary>
		public JwtBearerAuthenticationOptions()
		{
			this.JwtBearer = new JwtBearerAuthenticationSchemes();
		}

		/// <summary>
		///     Gets or sets the JWT Bearer authentication schemes.
		/// </summary>
		public JwtBearerAuthenticationSchemes JwtBearer { get; set; }
	}
}
