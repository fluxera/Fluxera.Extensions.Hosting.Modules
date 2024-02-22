namespace Fluxera.Extensions.Hosting.Modules.AspNetCore
{
	using Fluxera.Guards;
	using Microsoft.AspNetCore.WebUtilities;
	using System.Text;
	using JetBrains.Annotations;

	/// <summary>
	///     Extension methods for the <see cref="string" /> type.
	/// </summary>
	[PublicAPI]
	public static class StringExtensions
	{
		/// <summary>
		///     Encodes the given string for safe usage in URLs, f.e. for password reset or email confirmation tokens.
		/// </summary>
		public static string UrlEncode(this string str)
		{
			str = Guard.Against.NullOrWhiteSpace(str);

			return WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(str));
		}

		/// <summary>
		///		Decodes the given URL-safe string.
		/// </summary>
		public static string UrlDecode(this string str)
		{
			str = Guard.Against.NullOrWhiteSpace(str);

			return Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(str));
		}
	}
}
