namespace Fluxera.Extensions.Hosting.Modules.Messaging.Scheduler.Contributors
{
	using JetBrains.Annotations;
	using MassTransit;

	[UsedImplicitly]
	internal sealed class ConsumersContributor : IConsumersContributor
	{
		/// <inheritdoc />
		public void ConfigureConsumers(IRegistrationConfigurator configurator, IServiceConfigurationContext context)
		{
		}
	}
}
