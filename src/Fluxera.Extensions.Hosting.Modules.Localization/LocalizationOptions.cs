namespace Fluxera.Extensions.Hosting.Modules.Localization
{
	using JetBrains.Annotations;

	/// <summary>
	///     The options for the localization module.
	/// </summary>
	[PublicAPI]
	internal sealed class LocalizationOptions
	{
		/// <summary>
		///     Gets or sets the resources path om use.
		/// </summary>
		public string ResourcesPath { get; set; } = "Resources";
	}
}
