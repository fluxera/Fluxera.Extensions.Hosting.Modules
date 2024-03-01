namespace Fluxera.Extensions.Hosting.Modules.Scheduler.InMemory.Contributors
{
	using Fluxera.Extensions.Hosting.Modules.Scheduler;
	using JetBrains.Annotations;
	using Quartz;

	[UsedImplicitly]
	internal sealed class StoreContributor : IStoreContributor
	{
		/// <inheritdoc />
		public void ConfigureStore(IServiceCollectionQuartzConfigurator configurator, IServiceConfigurationContext context)
		{
			configurator.UseInMemoryStore();
		}
	}
}
