namespace Fluxera.Extensions.Hosting.Modules.Messaging.AzureServiceBus.Contributors
{
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
			AzureServiceBusMessagingOptions options = context.Services.GetOptions<AzureServiceBusMessagingOptions>();
			string connectionString = options.ConnectionStrings[options.ConnectionStringName];

			configurator.UsingAzureServiceBus((ctx, cfg) =>
			{
				bool isTransactionalOutboxModuleLoaded = context.Items.ContainsKey("IsTransactionalOutboxModuleLoaded");
				if(!isTransactionalOutboxModuleLoaded && options.InMemoryOutboxEnabled)
				{
					cfg.UseInMemoryOutbox();
				}

				cfg.Host(connectionString);

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

				// Configure consume filter that sets the context on a context provider.
				cfg.UseConsumeFilter(typeof(ConsumeContextConsumeFilter<>), ctx);

				cfg.ConfigureEndpoints(ctx);
			});
		}
	}
}
