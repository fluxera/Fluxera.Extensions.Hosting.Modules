namespace Fluxera.Extensions.Hosting.Modules.AspNetCore.Authentication
{
	using JetBrains.Annotations;

	/// <summary>
	///     The options of the authentication module.
	/// </summary>
	[PublicAPI]
	public sealed class AuthenticationOptions
	{
		/// <summary>
		///     Used as the fallback default scheme for all the other defaults.
		/// </summary>
		public string DefaultScheme { get; set; }
	}
}
