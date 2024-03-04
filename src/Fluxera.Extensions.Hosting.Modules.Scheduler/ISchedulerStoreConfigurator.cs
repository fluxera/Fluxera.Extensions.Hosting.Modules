namespace Fluxera.Extensions.Hosting.Modules.Scheduler
{
	using System;
	using JetBrains.Annotations;
	using Quartz;
	using Quartz.Spi;

	/// <summary>
	///		A store configurator for the scheduler.
	/// </summary>
	[PublicAPI]
	public interface ISchedulerStoreConfigurator
	{
		/// <summary>
		///		Use the in-memory store.
		/// </summary>
		/// <param name="configure"></param>
		void UseInMemoryStore(Action<SchedulerBuilder.InMemoryStoreOptions> configure = null);

		/// <summary>
		///		Use a persistent store.
		/// </summary>
		/// <param name="configure"></param>
		void UsePersistentStore(Action<SchedulerBuilder.PersistentStoreOptions> configure);

		/// <summary>
		///		Use a persistent store.
		/// </summary>
		/// <param name="configure"></param>
		void UsePersistentStore<T>(Action<SchedulerBuilder.PersistentStoreOptions> configure) 
			where T : class, IJobStore;
	}
}
