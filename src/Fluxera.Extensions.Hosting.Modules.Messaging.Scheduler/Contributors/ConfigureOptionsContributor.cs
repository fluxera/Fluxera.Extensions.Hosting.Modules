namespace Fluxera.Extensions.Hosting.Modules.Messaging.Scheduler.Contributors
{
	using Fluxera.Extensions.Hosting.Modules.Configuration;
	using MassTransit;
	using Microsoft.Extensions.DependencyInjection;

	internal sealed class ConfigureOptionsContributor : ConfigureOptionsContributorBase<SchedulerMessagingOptions>
	{
		/// <inheritdoc />
		public override string SectionName => "Messaging:Scheduler";

		/// <inheritdoc />
		protected override void AdditionalConfigure(IServiceConfigurationContext context, SchedulerMessagingOptions createdOptions)
		{
			context.Log("Configure(QuartzEndpointOptions)", services =>
			{
				services.Configure<QuartzEndpointOptions>(options =>
				{
					options.PrefetchCount = createdOptions.PrefetchCount;
					options.ConcurrentMessageLimit = createdOptions.ConcurrentMessageLimit;
					options.QueueName = "scheduler";
				});
			});
		}
	}
}
