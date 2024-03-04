namespace Fluxera.Extensions.Hosting.Modules.Scheduler.InMemory.Contributors
{
	using Fluxera.Extensions.Hosting.Modules.Scheduler;
	using JetBrains.Annotations;

	[UsedImplicitly]
	internal sealed class StoreContributor : IStoreContributor
	{
		/// <inheritdoc />
		public void ConfigureStore(ISchedulerStoreConfigurator configurator, IServiceConfigurationContext context)
		{
			configurator.UseInMemoryStore();
		}
	}
}
