namespace Fluxera.Extensions.Hosting.Modules.Configuration
{
	using System;
	using JetBrains.Annotations;
	using Microsoft.Extensions.Configuration;
	using Microsoft.Extensions.DependencyInjection;

	/// <summary>
	///     A contract for a configure contributor.
	/// </summary>
	[PublicAPI]
	public interface IConfigureContributor
	{
		/// <summary>
		///     Gets the name of the module aka. the app settings section key.
		/// </summary>
		string Name { get; }

		/// <summary>
		///     Gets the type of the options class.
		/// </summary>
		public Type OptionsType { get; }

		/// <summary>
		///     Configure the options using the given <see cref="IConfigurationSection" />.
		/// </summary>
		/// <param name="services"></param>
		/// <param name="section"></param>
		void Configure(IServiceCollection services, IConfigurationSection section);
	}
}
