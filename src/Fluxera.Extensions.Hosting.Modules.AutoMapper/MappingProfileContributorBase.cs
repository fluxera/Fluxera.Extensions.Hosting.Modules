namespace Fluxera.Extensions.Hosting.Modules.AutoMapper
{
	using JetBrains.Annotations;

	/// <summary>
	///     A base class that makes the service registration optional.
	/// </summary>
	[PublicAPI]
	public abstract class MappingProfileContributorBase : IMappingProfileContributor
	{
		/// <inheritdoc />
		public virtual void ConfigureProfileServices(IServiceConfigurationContext context)
		{
		}

		/// <inheritdoc />
		public abstract void ConfigureProfiles(AutoMapperOptions options, IApplicationInitializationContext context);
	}
}
