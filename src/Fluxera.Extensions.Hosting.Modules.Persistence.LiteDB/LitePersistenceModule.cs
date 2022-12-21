namespace Fluxera.Extensions.Hosting.Modules.Persistence.LiteDB
{
	using Fluxera.Extensions.Hosting.Modules.Persistence.LiteDB.Contributors;
	using JetBrains.Annotations;

	/// <summary>
	///     A module that enables LiteDB persistence.
	/// </summary>
	[PublicAPI]
	[DependsOn<PersistenceModule>]
	public sealed class LitePersistenceModule : ConfigureServicesModule
	{
		/// <inheritdoc />
		public override void PreConfigureServices(IServiceConfigurationContext context)
		{
			// Add the repository provider contributor.
			context.Log("AddRepositoryProviderContributor(LiteDB)",
				services => services.AddRepositoryProviderContributor<RepositoryProviderContributor>());
		}
	}
}
