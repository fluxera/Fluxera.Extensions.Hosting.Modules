﻿namespace Fluxera.Extensions.Hosting.Modules.Messaging.RabbitMQ.Contributors
{
	using System;
	using System.Text.Json;
	using Fluxera.Enumeration.SystemTextJson;
	using Fluxera.Extensions.Hosting.Modules.Configuration;
	using Fluxera.Extensions.Hosting.Modules.Messaging.Filters;
	using Fluxera.Spatial.SystemTextJson;
	using Fluxera.StronglyTypedId.SystemTextJson;
	using Fluxera.ValueObject.SystemTextJson;
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

			MessagingOptions messagingOptions = context.Services.GetOptions<MessagingOptions>();

			configurator.UsingRabbitMq((ctx, cfg) =>
			{
				bool isTransactionalOutboxModuleLoaded = context.Items.ContainsKey("IsTransactionalOutboxModuleLoaded");
				if(!isTransactionalOutboxModuleLoaded && options.InMemoryOutboxEnabled)
				{
					cfg.UseMessageScope(ctx);
					cfg.UseInMemoryOutbox(ctx);
				}

				if(messagingOptions.SchedulerEnabled)
				{
					cfg.UseMessageScheduler(new Uri("queue:scheduler"));
				}

				cfg.Host(rabbitConnectionString.Host, hostOptions =>
				{
					if(rabbitConnectionString.UseSsl)
					{
						throw new NotSupportedException("Using SSL is currently not supported.");
					}

					hostOptions.Username(rabbitConnectionString.Username);
					hostOptions.Password(rabbitConnectionString.Password);
				});

				cfg.ConfigureJsonSerializerOptions(serializerOptions =>
				{
					JsonSerializerOptions jsonSerializerOptions = new JsonSerializerOptions(serializerOptions);

					jsonSerializerOptions.UseSpatial();
					jsonSerializerOptions.UseEnumeration();
					jsonSerializerOptions.UsePrimitiveValueObject();
					jsonSerializerOptions.UseStronglyTypedId();

					return jsonSerializerOptions;
				});

				// Configure publish and send filters for message validation.
				cfg.UsePublishFilter(typeof(ValidatingPublishFilter<>), ctx);
				cfg.UseSendFilter(typeof(ValidatingSendFilter<>), ctx);

				// Configure publish and send filters for message authentication.
				cfg.UsePublishFilter(typeof(AuthenticatingPublishFilter<>), ctx);
				cfg.UseSendFilter(typeof(AuthenticatingSendFilter<>), ctx);

				// Configure publish and send filters for application context enrichment.
				cfg.UsePublishFilter(typeof(ApplicationContextEnrichingPublishFilter<>), ctx);
				cfg.UseSendFilter(typeof(ApplicationContextEnrichingSendFilter<>), ctx);

				// Configure consume filter that sets the context on a context provider.
				cfg.UseConsumeFilter(typeof(ConsumeContextConsumeFilter<>), ctx);

				cfg.ConfigureEndpoints(ctx);
			});
		}
	}
}
