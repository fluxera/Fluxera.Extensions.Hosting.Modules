namespace ConsoleSample.Contributors
{
	using ConsoleSample.Consumers;
	using Fluxera.Extensions.Hosting.Modules.Messaging;
	using MassTransit;

	internal sealed class ConsumersContributor : IConsumersContributor
	{
		/// <inheritdoc />
		public void ConfigureConsumers(IRegistrationConfigurator configurator)
		{
			configurator.AddConsumer<PackageSentConsumer>();
		}
	}
}
