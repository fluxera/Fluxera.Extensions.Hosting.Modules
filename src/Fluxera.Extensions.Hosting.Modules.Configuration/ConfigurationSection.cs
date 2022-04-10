namespace Fluxera.Extensions.Hosting.Modules.Configuration
{
	using Fluxera.Guards;
	using JetBrains.Annotations;

	/// <summary>
	///     Utility class that helps getting the correct section name.
	/// </summary>
	[PublicAPI]
	public static class ConfigurationSection
	{
		/// <summary>
		///     The prefix for every modules configuration section.
		/// </summary>
		public const string ModuleSectionNamePrefix = "Hosting:Modules";

		/// <summary>
		///     Gets the configuration section for the given module.
		/// </summary>
		/// <param name="moduleSectionName"></param>
		/// <returns></returns>
		public static string GetSectionName(string moduleSectionName)
		{
			Guard.Against.NullOrWhiteSpace(moduleSectionName, nameof(moduleSectionName));

			return $"{ModuleSectionNamePrefix}:{moduleSectionName}";
		}
	}
}
