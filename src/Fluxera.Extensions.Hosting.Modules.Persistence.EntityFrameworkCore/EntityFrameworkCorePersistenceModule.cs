namespace Fluxera.Extensions.Hosting.Modules.Persistence.EntityFrameworkCore
{
	using Fluxera.Extensions.Hosting.Modules.Persistence.EntityFrameworkCore.Contributors;
	using JetBrains.Annotations;

	/// <summary>
	///     A module that enables EntityFramework Core persistence.
	/// </summary>
	[PublicAPI]
	[DependsOn(typeof(PersistenceModule))]
	public sealed class EntityFrameworkCorePersistenceModule : ConfigureServicesModule
	{
		/// <inheritdoc />
		public override void PreConfigureServices(IServiceConfigurationContext context)
		{
			// Add the repository provider contributor.
			context.Log("AddRepositoryProviderContributor(EntityFrameworkCore)",
				services => services.AddRepositoryProviderContributor<RepositoryProviderContributor>());
		}
	}
}
