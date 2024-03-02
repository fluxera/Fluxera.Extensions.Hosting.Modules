namespace Fluxera.Extensions.Hosting.Modules.Scheduler.MongoDB.Contributors
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
