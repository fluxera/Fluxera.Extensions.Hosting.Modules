namespace Fluxera.Extensions.Hosting.Modules.AspNetCore.HttpApi
{
	using JetBrains.Annotations;

	/// <summary>
	///     The HTTP API description options.
	/// </summary>
	[PublicAPI]
	public sealed class HttpApiDescription
	{
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
