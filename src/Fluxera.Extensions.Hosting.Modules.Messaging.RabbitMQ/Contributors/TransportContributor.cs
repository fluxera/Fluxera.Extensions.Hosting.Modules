﻿namespace Fluxera.Extensions.Hosting.Modules.Messaging.RabbitMQ.Contributors
{
	using Fluxera.Extensions.Hosting.Modules.Configuration;
	using Fluxera.Extensions.Hosting.Modules.Messaging.Filters;
	using JetBrains.Annotations;
	using MassTransit;

	[UsedImplicitly]
	internal sealed class TransportContributor : ITransportContributor
	{
		/// <inheritdoc />
		public void ConfigureTransport(IBusRegistrationConfigurator configurator, IServiceConfigurationContext context)
		{
			RabbitMqMessagingOptions options = context.Services.GetOptions<RabbitMqMessagingOptions>();
			string connectionString = options.ConnectionStrings[options.ConnectionStringName];

			RabbitMqConnectionString rabbitConnectionString = new RabbitMqConnectionString(connectionString);

			configurator.UsingRabbitMq((ctx, cfg) =>
			{
				cfg.Host(rabbitConnectionString.Host, hostOptions =>
				{
					hostOptions.Username(rabbitConnectionString.Username);
					hostOptions.Password(rabbitConnectionString.Password);
				});

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