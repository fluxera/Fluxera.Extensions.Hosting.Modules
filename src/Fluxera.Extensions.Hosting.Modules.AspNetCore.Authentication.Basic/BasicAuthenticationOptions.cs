namespace Fluxera.Extensions.Hosting.Modules.AspNetCore.Authentication.Basic
{
	using JetBrains.Annotations;

	/// <summary>
	///     The options for the Basic authentication module.
	/// </summary>
	[PublicAPI]
	public sealed class BasicAuthenticationOptions
	{
		/// <summary>
		///     Creates a new instance of the <see cref="BasicAuthenticationOptions" /> type.
		/// </summary>
		public BasicAuthenticationOptions()
		{
			this.Basic = new BasicAuthenticationSchemes();
		}

		/// <summary>
		///     Gets or sets the Basic authentication schemes.
		/// </summary>
		public BasicAuthenticationSchemes Basic { get; set; }
	}
}
