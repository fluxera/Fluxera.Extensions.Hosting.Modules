namespace Fluxera.Extensions.Hosting.Modules.AspNetCore.Authentication.Basic
{
	using JetBrains.Annotations;
	using Microsoft.Extensions.Configuration;

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
			this.Schemes = new BasicAuthenticationSchemes();
		}

		/// <summary>
		///     Gets or sets the Basic authentication schemes.
		/// </summary>
		[ConfigurationKeyName("Basic")]
		public BasicAuthenticationSchemes Schemes { get; set; }
	}
}
