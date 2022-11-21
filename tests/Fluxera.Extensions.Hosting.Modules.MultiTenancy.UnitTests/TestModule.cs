namespace Fluxera.Extensions.Hosting.Modules.MultiTenancy.UnitTests
{
	using Fluxera.Extensions.Hosting.Modules.Persistence;
	using Fluxera.Extensions.Hosting.Modules.Persistence.InMemory;

	[DependsOn(typeof(MultiTenancyModule))]
	[DependsOn(typeof(InMemoryPersistenceModule))]
	public class TestModule : ConfigureServicesModule
	{
		/// <inheritdoc />
		public override void PreConfigureServices(IServiceConfigurationContext context)
		{
			context.Services.AddRepositoryContributor<RepositoryContributor>("Test");

			context.Services.AddRepositoryContextContributor<RepositoryContextContributor>("Test");
		}
	}
}
