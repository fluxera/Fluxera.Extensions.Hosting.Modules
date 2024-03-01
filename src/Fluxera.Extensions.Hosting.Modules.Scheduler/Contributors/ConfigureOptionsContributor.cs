namespace Fluxera.Extensions.Hosting.Modules.Scheduler.Contributors
{
	using Fluxera.Extensions.Hosting.Modules.Configuration;
	using Microsoft.Extensions.DependencyInjection;
	using Quartz;

	internal sealed class ConfigureOptionsContributor : ConfigureOptionsContributorBase<SchedulerOptions>
	{
		/// <inheritdoc />
		public override string SectionName => "Scheduler";

		/// <inheritdoc />
		protected override void AdditionalConfigure(IServiceConfigurationContext context, SchedulerOptions createdOptions)
		{
			context.Log("Configure(QuartzHostedServiceOptions)", services =>
			{
				services.Configure<QuartzHostedServiceOptions>(options =>
				{
					options.WaitForJobsToComplete = createdOptions.WaitForJobsToComplete;
					options.StartDelay = createdOptions.StartDelay;
					options.AwaitApplicationStarted = createdOptions.AwaitApplicationStarted;
				});
			});
		}
	}
}
