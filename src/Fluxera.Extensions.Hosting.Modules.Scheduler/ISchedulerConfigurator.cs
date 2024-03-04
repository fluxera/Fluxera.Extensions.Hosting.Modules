namespace Fluxera.Extensions.Hosting.Modules.Scheduler
{
	using JetBrains.Annotations;
	using Quartz;
	using System;

	/// <summary>
	///		A job/trigger configurator for the scheduler.
	/// </summary>
	[PublicAPI]
	public interface ISchedulerConfigurator
	{
		/// <summary>
		///		Adds a job to the scheduler.
		/// </summary>
		/// <param name="jobType"></param>
		/// <param name="configure"></param>
		void AddJob(Type jobType, Action<JobBuilder> configure);

		/// <summary>
		///		Adds a job to the scheduler.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="configure"></param>
		void AddJob<T>(Action<JobBuilder> configure) where T : IJob;

		/// <summary>
		///		Adds a trigger to the scheduler.
		/// </summary>
		/// <param name="configure"></param>
		void AddTrigger(Action<TriggerBuilder> configure);

		/// <summary>
		///		Adds a scheduler listener.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		void AddSchedulerListener<T>() where T : class, ISchedulerListener;

		/// <summary>
		///		Adds a scheduler listener.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="implementationInstance"></param>
		void AddSchedulerListener<T>(T implementationInstance) where T : class, ISchedulerListener;

		/// <summary>
		///		Adds a scheduler listener.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="implementationFactory"></param>
		void AddSchedulerListener<T>(Func<IServiceProvider, T> implementationFactory) where T : class, ISchedulerListener;

		/// <summary>
		///		Adds a job listener.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="matchers"></param>
		void AddJobListener<T>(params IMatcher<JobKey>[] matchers) where T : class, IJobListener;

		/// <summary>
		///		Adds a job listener.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="implementationInstance"></param>
		/// <param name="matchers"></param>
		void AddJobListener<T>(T implementationInstance, params IMatcher<JobKey>[] matchers) where T : class, IJobListener;

		/// <summary>
		///		Adds a job listener.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="implementationFactory"></param>
		/// <param name="matchers"></param>
		void AddJobListener<T>(Func<IServiceProvider, T> implementationFactory, params IMatcher<JobKey>[] matchers) where T : class, IJobListener;

		/// <summary>
		///		Adds a trigger listener.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="matchers"></param>
		void AddTriggerListener<T>(params IMatcher<TriggerKey>[] matchers) where T : class, ITriggerListener;

		/// <summary>
		///		Adds a trigger listener.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="implementationInstance"></param>
		/// <param name="matchers"></param>
		void AddTriggerListener<T>(T implementationInstance, params IMatcher<TriggerKey>[] matchers) where T : class, ITriggerListener;

		/// <summary>
		///		Adds a trigger listener.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="implementationFactory"></param>
		/// <param name="matchers"></param>
		void AddTriggerListener<T>(Func<IServiceProvider, T> implementationFactory, params IMatcher<TriggerKey>[] matchers) where T : class, ITriggerListener;
	}
}
