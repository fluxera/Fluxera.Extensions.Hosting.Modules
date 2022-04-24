namespace Fluxera.Extensions.Hosting.Modules.AspNetCore.Swagger
{
	using JetBrains.Annotations;

	/// <summary>
	///     The API description options.
	/// </summary>
	[PublicAPI]
	public sealed class SwaggerApiDescription
	{
		/// <summary>
		///     Gets or sets the API version.
		/// </summary>
		public string Version { get; set; }

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
