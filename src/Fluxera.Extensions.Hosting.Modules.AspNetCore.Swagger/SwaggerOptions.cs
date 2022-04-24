namespace Fluxera.Extensions.Hosting.Modules.AspNetCore.Swagger
{
	using JetBrains.Annotations;

	/// <summary>
	///     The options for the Swagger module.
	/// </summary>
	[PublicAPI]
	public sealed class SwaggerOptions
	{
		/// <summary>
		///     Creates a new instance of the <see cref="SwaggerOptions" /> type.
		/// </summary>
		public SwaggerOptions()
		{
			this.Enabled = true;
			this.Descriptions = new SwaggerApiDescriptions();
		}

		/// <summary>
		///     Flag, indicating if Swagger is enabled.
		/// </summary>
		public bool Enabled { get; set; }

		/// <summary>
		///     Gets or sets the Swagger API descriptions.
		/// </summary>
		public SwaggerApiDescriptions Descriptions { get; set; }
	}
}
