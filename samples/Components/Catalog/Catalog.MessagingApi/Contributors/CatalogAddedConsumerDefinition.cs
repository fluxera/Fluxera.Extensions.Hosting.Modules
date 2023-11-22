namespace Catalog.MessagingApi.Contributors
{
	using Catalog.MessagingApi.Consumers;
	using JetBrains.Annotations;
	using MassTransit;

	[UsedImplicitly]
	internal sealed class CatalogAddedConsumerDefinition : ConsumerDefinition<ProductAddedConsumer>
	{
		/// <inheritdoc />
		protected override void ConfigureConsumer(
			IReceiveEndpointConfigurator endpointConfigurator,
			IConsumerConfigurator<ProductAddedConsumer> consumerConfigurator,
			IRegistrationContext context)
		{
			endpointConfigurator.UseMessageRetry(r => r.Intervals(10, 50, 100, 1000, 1000, 1000, 1000, 1000));
		}
	}
}
