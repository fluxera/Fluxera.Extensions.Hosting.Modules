namespace Fluxera.Extensions.Hosting.Modules.AspNetCore.HttpApi
{
	using JetBrains.Annotations;

	/// <summary>
	///     The options for the versioning module.
	/// </summary>
	[PublicAPI]
	public sealed class VersioningOptions
	{
		/// <summary>
		///     Creates a new instance of the <see cref="VersioningOptions" /> type.
		/// </summary>
		public VersioningOptions()
		{
			this.Enabled = true;
		}

		/// <summary>
		///     Flag, indicating if Swagger is enabled.
		/// </summary>
		public bool Enabled { get; set; }
	}
}
