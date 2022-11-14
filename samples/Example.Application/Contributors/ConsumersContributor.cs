namespace Example.Application.Contributors
{
	using Example.Application.Consumers;
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
			configurator.AddConsumer<ExampleAddedConsumer>();
			configurator.AddConsumer<ExampleUpdatedConsumer>();
		}
	}
}
