namespace Fluxera.Extensions.Hosting.Modules.AspNetCore.Authentication.ApiKey
{
	using JetBrains.Annotations;
	using Microsoft.Extensions.Configuration;

	/// <summary>
	///     The options for the ApiKey authentication module.
	/// </summary>
	[PublicAPI]
	public sealed class ApiKeyAuthenticationOptions
	{
		/// <summary>
		///     Creates a new instance of the <see cref="ApiKeyAuthenticationOptions" /> type.
		/// </summary>
		public ApiKeyAuthenticationOptions()
		{
			this.Schemes = new ApiKeyAuthenticationSchemes();
		}

		/// <summary>
		///     Gets or sets the ApiKey authentication schemes.
		/// </summary>
		[ConfigurationKeyName("ApiKey")]
		public ApiKeyAuthenticationSchemes Schemes { get; set; }
	}
}
