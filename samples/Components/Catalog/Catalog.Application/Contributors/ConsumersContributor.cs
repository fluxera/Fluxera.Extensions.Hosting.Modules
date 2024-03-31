namespace Catalog.Application.Contributors
{
	using Catalog.Application.Consumers;
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
			configurator.AddConsumer<ProductAddedConsumer, ProductAddedConsumerDefinition>();
			configurator.AddConsumer<ProductUpdatedConsumer>();
		}
	}
}
