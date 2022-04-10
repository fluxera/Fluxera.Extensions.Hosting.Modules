namespace Fluxera.Extensions.Hosting.Modules.Configuration
{
	using JetBrains.Annotations;
	using Microsoft.Extensions.Configuration;

	/// <summary>
	///     A contract for a configure contributor
	/// </summary>
	[PublicAPI]
	public interface IConfigureOptionsContributor
	{
		/// <summary>
		///     Gets the name of the module aka. the app settings section key.
		/// </summary>
		string SectionName { get; }

		/// <summary>
		///     Configure the options using the given <see cref="IConfigurationSection" />.
		/// </summary>
		/// <param name="context"></param>
		void Configure(IServiceConfigurationContext context);
	}
}
