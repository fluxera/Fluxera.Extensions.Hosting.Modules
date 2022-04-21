namespace Fluxera.Extensions.Hosting.Modules.AspNetCore.Authentication.ApiKey
{
	using JetBrains.Annotations;

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
			this.ApiKey = new ApiKeyAuthenticationSchemes();
		}

		/// <summary>
		///     Gets or sets the ApiKey authentication schemes.
		/// </summary>
		public ApiKeyAuthenticationSchemes ApiKey { get; set; }
	}
}
