namespace Fluxera.Extensions.Hosting.Modules.AspNetCore.HttpApi
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
		}

		/// <summary>
		///     Flag, indicating if Swagger is enabled.
		/// </summary>
		public bool Enabled { get; set; }
	}
}
