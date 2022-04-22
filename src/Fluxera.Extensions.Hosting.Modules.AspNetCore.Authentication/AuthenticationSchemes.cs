namespace Fluxera.Extensions.Hosting.Modules.AspNetCore.Authentication
{
	using System.Collections.Generic;
	using Fluxera.Utilities.Extensions;
	using JetBrains.Annotations;

	/// <summary>
	///     A static class that contains constants.
	/// </summary>
	[PublicAPI]
	public static class AuthenticationSchemes
	{
		/// <summary>
		///     The name of the default scheme name.
		/// </summary>
		public const string DefaultSchemeName = "Default";
	}

	/// <summary>
	///     A dictionary containing authentication schemes.
	/// </summary>
	[PublicAPI]
	public abstract class AuthenticationSchemes<T> : Dictionary<string, T>
	{
		/// <summary>
		///     Gets or sets the default scheme.
		/// </summary>
		public T Default
		{
			get => this.GetOrDefault(AuthenticationSchemes.DefaultSchemeName);
			set => this[AuthenticationSchemes.DefaultSchemeName] = value;
		}
	}
}
