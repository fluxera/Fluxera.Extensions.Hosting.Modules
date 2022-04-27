namespace Fluxera.Extensions.Hosting.Modules.Messaging.InMemory.Contributors
{
	using Fluxera.Extensions.Hosting.Modules.Messaging.Filters;
	using JetBrains.Annotations;
	using MassTransit;

	[UsedImplicitly]
	internal sealed class TransportContributor : ITransportContributor
	{
		/// <inheritdoc />
		public void ConfigureTransport(IBusRegistrationConfigurator configurator, IServiceConfigurationContext context)
		{
			configurator.UsingInMemory((ctx, cfg) =>
			{
				// Configure publish and send filters for message validation.
				cfg.UsePublishFilter(typeof(ValidatingPublishFilter<>), ctx);
				cfg.UseSendFilter(typeof(ValidatingSendFilter<>), ctx);

				// Configure publish and send filters for message authentication.
				cfg.UsePublishFilter(typeof(AuthenticatingPublishFilter<>), ctx);
				cfg.UseSendFilter(typeof(AuthenticatingSendFilter<>), ctx);

				// Configure consume filter that sets the context on a context provider.
				cfg.UseConsumeFilter(typeof(ConsumeContextConsumeFilter<>), ctx);

				cfg.ConfigureEndpoints(ctx);
			});
		}
	}
}
