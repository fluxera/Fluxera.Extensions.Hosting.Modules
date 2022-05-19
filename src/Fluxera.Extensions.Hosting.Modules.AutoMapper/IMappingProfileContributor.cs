namespace Fluxera.Extensions.Hosting.Modules.AutoMapper
{
	using JetBrains.Annotations;

	/// <summary>
	///     A contract for contributors that provide AutoMapper profiles.
	/// </summary>
	[PublicAPI]
	public interface IMappingProfileContributor
	{
		/// <summary>
		///     Configure the mapper profiles as services. This is optional.
		/// </summary>
		/// <param name="context"></param>
		void ConfigureProfileServices(IServiceConfigurationContext context);

		/// <summary>
		///     Configures the profiles to use for mapping.
		/// </summary>
		/// <param name="options"></param>
		/// <param name="context"></param>
		void ConfigureProfiles(AutoMapperOptions options, IApplicationInitializationContext context);
	}
}
