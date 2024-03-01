namespace Fluxera.Extensions.Hosting.Modules.Scheduler
{
	using System;
	using System.Threading;
	using System.Threading.Tasks;
	using Microsoft.Extensions.Diagnostics.HealthChecks;
	using Quartz;

	/// <summary>
	///		TODO: Use the one provided in Quartz 4.0
	/// </summary>
	internal sealed class QuartzHealthCheck : IHealthCheck
	{
		private readonly ISchedulerFactory schedulerFactory;

		public QuartzHealthCheck(ISchedulerFactory schedulerFactory)
		{
			this.schedulerFactory = schedulerFactory ?? throw new ArgumentNullException(nameof(schedulerFactory));
		}

		async Task<HealthCheckResult> IHealthCheck.CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken)
		{
			IScheduler scheduler = await this.schedulerFactory.GetScheduler(cancellationToken).ConfigureAwait(false);
			if(!scheduler.IsStarted)
			{
				return HealthCheckResult.Unhealthy("Quartz scheduler is not running.");
			}

			try
			{
				// Ask for a job we know doesn't exist
				await scheduler.CheckExists(new JobKey(Guid.NewGuid().ToString()), cancellationToken).ConfigureAwait(false);
			}
			catch(SchedulerException)
			{
				return HealthCheckResult.Unhealthy("Quartz scheduler cannot connect to the store.");
			}

			return HealthCheckResult.Healthy("Quartz scheduler is ready.");
		}
	}
}
