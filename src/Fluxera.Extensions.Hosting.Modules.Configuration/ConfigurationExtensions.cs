namespace Fluxera.Extensions.Hosting.Modules.Configuration
{
	using JetBrains.Annotations;
	using Microsoft.Extensions.Configuration;

	/// <summary>
	///     Extensions methods for the type <see cref="IConfiguration" />.
	/// </summary>
	[PublicAPI]
	public static class ConfigurationExtensions
	{
		/// <summary>
		///     Gets the options for a module from the configuration.
		/// </summary>
		/// <typeparam name="TOptions"></typeparam>
		/// <param name="configuration"></param>
		/// <param name="name"></param>
		/// <returns></returns>
		public static TOptions Get<TOptions>(this IConfiguration configuration, string name)
		{
			IConfigurationSection section = configuration.GetSection($"Hosting:Modules:{name}");
			return section.Get<TOptions>();
		}
	}
}
