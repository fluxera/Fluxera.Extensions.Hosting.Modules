namespace Fluxera.Extensions.Hosting.Modules.AspNetCore.HttpApi
{
	using System;
	using JetBrains.Annotations;

	/// <summary>
	///     The HTTP API description options.
	/// </summary>
	[PublicAPI]
	public sealed class HttpApiDescription
	{
		/// <summary>
		///     Gets or sets the API version.
		/// </summary>
		public Version Version { get; set; } = new Version(1, 0);

		/// <summary>
		///     Gets or sets the API title.
		/// </summary>
		public string Title { get; set; }

		/// <summary>
		///     Gets or sets the API description.
		/// </summary>
		public string Description { get; set; }
	}
}
