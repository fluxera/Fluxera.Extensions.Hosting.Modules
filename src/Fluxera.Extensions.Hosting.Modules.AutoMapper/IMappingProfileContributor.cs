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
		///     Configures the profiles to use for mapping.
		/// </summary>
		/// <param name="options"></param>
		void ConfigureProfiles(AutoMapperOptions options);
	}
}
