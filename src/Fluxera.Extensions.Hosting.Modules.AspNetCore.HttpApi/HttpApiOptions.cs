namespace Fluxera.Extensions.Hosting.Modules.AspNetCore.HttpApi
{
	using System;
	using System.Linq;
	using JetBrains.Annotations;

	/// <summary>
	///     The options for the HTTP API module.
	/// </summary>
	[PublicAPI]
	public sealed class HttpApiOptions
	{
		/// <summary>
		///     Creates a new instance of the <see cref="HttpApiOptions" /> type.
		/// </summary>
		public HttpApiOptions()
		{
			this.Descriptions = new HttpApiDescriptions();
			this.Swagger = new SwaggerOptions();
		}

		/// <summary>
		///     Gets or sets the name of the HTTP API.
		/// </summary>
		public string Name { get; set; } = "API";

		/// <summary>
		///     Gets or sets the default API version.
		/// </summary>
		public Version DefaultVersion { get; set; } = new Version(1, 0);

		/// <summary>
		///     Gets or sets the HTTP API descriptions.
		/// </summary>
		public HttpApiDescriptions Descriptions { get; set; }

		/// <summary>
		///     Gets or sets the Swagger options.
		/// </summary>
		public SwaggerOptions Swagger { get; set; }

		/// <summary>
		///     Flag, indicating if the versioning is enabled.
		/// </summary>
		public bool IsVersioningEnabled => this.Descriptions.Any();
	}
}
