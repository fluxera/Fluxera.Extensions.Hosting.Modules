namespace Fluxera.Extensions.Hosting.Modules.Scheduler
{
	using System;
	using Microsoft.Extensions.DependencyInjection;
	using Quartz;

	internal sealed class SchedulerConfigurator : ISchedulerConfigurator
	{
		private readonly IServiceCollectionQuartzConfigurator configurator;
		private readonly IServiceCollection services;

		public SchedulerConfigurator(IServiceCollectionQuartzConfigurator configurator, IServiceCollection services)
		{
			this.configurator = configurator;
			this.services = services;
		}

		/// <inheritdoc />
		public void AddJob(Type jobType, Action<JobBuilder> configure)
		{
			this.services.Configure<QuartzOptions>(options =>
			{
				options.AddJob(jobType, configure);
			});
		}

		/// <inheritdoc />
		public void AddJob<T>(Action<JobBuilder> configure) where T : IJob
		{
			this.services.Configure<QuartzOptions>(options =>
			{
				options.AddJob<T>(configure);
			});
		}

		/// <inheritdoc />
		public void AddTrigger(Action<TriggerBuilder> configure)
		{
			this.services.Configure<QuartzOptions>(options =>
			{
				options.AddTrigger(configure);
			});
		}

		/// <inheritdoc />
		public void AddSchedulerListener<T>() where T : class, ISchedulerListener
		{
			this.configurator.AddSchedulerListener<T>();
		}

		/// <inheritdoc />
		public void AddSchedulerListener<T>(T implementationInstance) where T : class, ISchedulerListener
		{
			this.configurator.AddSchedulerListener(implementationInstance);
		}

		/// <inheritdoc />
		public void AddSchedulerListener<T>(Func<IServiceProvider, T> implementationFactory) where T : class, ISchedulerListener
		{
			this.configurator.AddSchedulerListener(implementationFactory);
		}

		/// <inheritdoc />
		public void AddJobListener<T>(params IMatcher<JobKey>[] matchers) where T : class, IJobListener
		{
			this.configurator.AddJobListener<T>(matchers);
		}

		/// <inheritdoc />
		public void AddJobListener<T>(T implementationInstance, params IMatcher<JobKey>[] matchers) where T : class, IJobListener
		{
			this.configurator.AddJobListener(implementationInstance, matchers);
		}

		/// <inheritdoc />
		public void AddJobListener<T>(Func<IServiceProvider, T> implementationFactory, params IMatcher<JobKey>[] matchers) where T : class, IJobListener
		{
			this.configurator.AddJobListener(implementationFactory, matchers);
		}

		/// <inheritdoc />
		public void AddTriggerListener<T>(params IMatcher<TriggerKey>[] matchers) where T : class, ITriggerListener
		{
			this.configurator.AddTriggerListener<T>(matchers);
		}

		/// <inheritdoc />
		public void AddTriggerListener<T>(T implementationInstance, params IMatcher<TriggerKey>[] matchers) where T : class, ITriggerListener
		{
			this.configurator.AddTriggerListener(implementationInstance, matchers);
		}

		/// <inheritdoc />
		public void AddTriggerListener<T>(Func<IServiceProvider, T> implementationFactory, params IMatcher<TriggerKey>[] matchers) where T : class, ITriggerListener
		{
			this.configurator.AddTriggerListener(implementationFactory, matchers);
		}
	}
}
