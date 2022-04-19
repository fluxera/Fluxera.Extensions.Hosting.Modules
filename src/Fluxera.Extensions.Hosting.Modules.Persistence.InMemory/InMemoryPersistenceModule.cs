namespace Fluxera.Extensions.Hosting.Modules.Persistence.InMemory
{
	using Fluxera.Extensions.Hosting.Modules.Persistence.InMemory.Contributors;
	using JetBrains.Annotations;

	/// <summary>
	///     A module that enables in-memory persistence.
	/// </summary>
	[PublicAPI]
	[DependsOn(typeof(PersistenceModule))]
	public sealed class InMemoryPersistenceModule : ConfigureServicesModule
	{
		/// <inheritdoc />
		public override void PreConfigureServices(IServiceConfigurationContext context)
		{
			// Add the repository provider contributor.
			context.Log("AddRepositoryProviderContributor(InMemory)",
				services => services.AddRepositoryProviderContributor<RepositoryProviderContributor>());
		}
	}
}
