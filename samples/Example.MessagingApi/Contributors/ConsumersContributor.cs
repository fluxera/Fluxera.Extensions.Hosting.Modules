namespace Example.MessagingApi.Contributors
{
	using Example.MessagingApi.Consumers;
	using Fluxera.Extensions.Hosting;
	using Fluxera.Extensions.Hosting.Modules.Messaging;
	using JetBrains.Annotations;
	using MassTransit;

	[UsedImplicitly]
	internal sealed class ConsumersContributor : IConsumersContributor
	{
		/// <inheritdoc />
		public void ConfigureConsumers(IRegistrationConfigurator configurator, IServiceConfigurationContext context)
		{
			configurator.AddConsumer<ExampleAddedConsumer, ExampleAddedConsumerDefinition>();
			configurator.AddConsumer<ExampleUpdatedConsumer>();
		}
	}
}
