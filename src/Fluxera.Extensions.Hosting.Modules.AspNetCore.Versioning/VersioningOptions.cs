namespace Fluxera.Extensions.Hosting.Modules.AspNetCore.Versioning
{
	using System;
	using JetBrains.Annotations;

	/// <summary>
	///     The options for the API versioning module.
	/// </summary>
	[PublicAPI]
	public sealed class VersioningOptions
	{
		/// <summary>
		///     Creates anew instance of the <see cref="VersioningOptions" /> type.
		/// </summary>
		public VersioningOptions()
		{
			this.DefaultApiVersion = new Version(1, 0);
		}

		/// <summary>
		///     Gets or sets the default API version.
		/// </summary>
		public Version DefaultApiVersion { get; set; }
	}
}
