namespace Fluxera.Extensions.Hosting.Modules.Scheduler
{
	using System;
	using Quartz;
	using Quartz.Spi;

	internal sealed class SchedulerStoreConfigurator : ISchedulerStoreConfigurator
	{
		private readonly IServiceCollectionQuartzConfigurator configurator;

		public SchedulerStoreConfigurator(IServiceCollectionQuartzConfigurator configurator)
		{
			this.configurator = configurator;
		}

		/// <inheritdoc />
		public void UseInMemoryStore(Action<SchedulerBuilder.InMemoryStoreOptions> configure = null)
		{
			this.configurator.UseInMemoryStore(configure);
		}

		/// <inheritdoc />
		public void UsePersistentStore(Action<SchedulerBuilder.PersistentStoreOptions> configure)
		{
			this.configurator.UsePersistentStore(configure);
		}

		/// <inheritdoc />
		public void UsePersistentStore<T>(Action<SchedulerBuilder.PersistentStoreOptions> configure) where T : class, IJobStore
		{
			this.configurator.UsePersistentStore<T>(configure);
		}
	}
}
