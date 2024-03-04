namespace Fluxera.Extensions.Hosting.Modules.Scheduler.EntityFrameworkCore.Contributors
{
	using Fluxera.Extensions.Hosting.Modules.Scheduler;
	using JetBrains.Annotations;

	[UsedImplicitly]
	internal sealed class StoreContributor : IStoreContributor
	{
		/// <inheritdoc />
		public void ConfigureStore(ISchedulerStoreConfigurator configurator, IServiceConfigurationContext context)
		{
			// TODO: When EF Core is supported.
		}
	}
}
