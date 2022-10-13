namespace Fluxera.Extensions.Hosting.Modules.AspNetCore
{
	using System;
	using JetBrains.Annotations;

	/// <summary>
	///     The options for the ASP.NET Core module.
	/// </summary>
	[PublicAPI]
	public sealed class AspNetCoreOptions
	{
		/// <summary>
		///     Gets or sets the base url of the application, f.e http://localhost:5000 or https://www.fluxera.com
		/// </summary>
		public Uri BaseUrl { get; set; }
	}
}
