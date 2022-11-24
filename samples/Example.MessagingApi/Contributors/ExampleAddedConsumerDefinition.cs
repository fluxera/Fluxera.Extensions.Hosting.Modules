namespace Example.MessagingApi.Contributors
{
	using Example.MessagingApi.Consumers;
	using JetBrains.Annotations;
	using MassTransit;

	[UsedImplicitly]
	internal sealed class ExampleAddedConsumerDefinition : ConsumerDefinition<ExampleAddedConsumer>
	{
		/// <inheritdoc />
		protected override void ConfigureConsumer(IReceiveEndpointConfigurator endpointConfigurator,
			IConsumerConfigurator<ExampleAddedConsumer> consumerConfigurator)
		{
			endpointConfigurator.UseMessageRetry(r => r.Intervals(10, 50, 100, 1000, 1000, 1000, 1000, 1000));
		}
	}
}
