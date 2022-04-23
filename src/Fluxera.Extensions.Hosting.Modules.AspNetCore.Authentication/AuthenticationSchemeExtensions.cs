namespace Fluxera.Extensions.Hosting.Modules.AspNetCore.Authentication
{
	using Fluxera.Guards;
	using JetBrains.Annotations;

	/// <summary>
	///     Extension methods for calculating the authentication scheme name to use.
	/// </summary>
	[PublicAPI]
	public static class AuthenticationSchemeExtensions
	{
		/// <summary>
		///     Calculates the authentication scheme name.
		/// </summary>
		/// <param name="key">The scheme name from the configuration.</param>
		/// <param name="authenticationSchemeDefault">The authentication scheme default, f.e 'Basic' or 'Bearer'.</param>
		/// <returns></returns>
		public static string CalculateSchemeName(this string key, string authenticationSchemeDefault)
		{
			Guard.Against.NullOrWhiteSpace(key, nameof(key));

			string schemeName = $"{authenticationSchemeDefault}-{key}";
			if((key == AuthenticationSchemes.DefaultSchemeName) || (key == authenticationSchemeDefault))
			{
				schemeName = authenticationSchemeDefault;
			}

			return schemeName;
		}
	}
}
