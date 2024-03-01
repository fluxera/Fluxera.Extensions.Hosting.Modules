namespace Fluxera.Extensions.Hosting.Modules.Messaging.Scheduler.Contributors
{
	using JetBrains.Annotations;
	using MassTransit;
	using MassTransit.Configuration;
	using MassTransit.QuartzIntegration;
	using Microsoft.Extensions.DependencyInjection.Extensions;

	[UsedImplicitly]
	internal sealed class ConsumersContributor : IConsumersContributor
	{
		/// <inheritdoc />
		public void ConfigureConsumers(IRegistrationConfigurator configurator, IServiceConfigurationContext context)
		{
			configurator.AddBusObserver<QuartzBusObserver>();
			
			configurator.TryAddSingleton<QuartzEndpointDefinition>();

			configurator.AddConsumer<ScheduleMessageConsumer, ScheduleMessageConsumerDefinition>();
			configurator.AddConsumer<CancelScheduledMessageConsumer, CancelScheduledMessageConsumerDefinition>();
			configurator.AddConsumer<PauseScheduledMessageConsumer, PauseScheduledMessageConsumerDefinition>();
			configurator.AddConsumer<ResumeScheduledMessageConsumer, ResumeScheduledMessageConsumerDefinition>();
		}
	}
}
