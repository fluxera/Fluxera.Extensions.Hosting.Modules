namespace Fluxera.Extensions.Hosting.Modules.AspNetCore.Cors
{
	using System.Collections.Generic;
	using JetBrains.Annotations;

	/// <summary>
	///     The options for the CORS module.
	/// </summary>
	[PublicAPI]
	public sealed class CorsOptions
	{
		/// <summary>
		///     The name of the default CORS policy.
		/// </summary>
		public const string DefaultCorsPolicyName = "Default";

		/// <summary>
		///     Creates a new instance of the <see cref="CorsOptions" /> type.
		/// </summary>
		public CorsOptions()
		{
			this.Origins = new List<string>();
		}

		/// <summary>
		///     Gets or sets the CORS origins.
		/// </summary>
		public IList<string> Origins { get; set; }
	}
}
