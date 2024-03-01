namespace Fluxera.Extensions.Hosting.Modules.Messaging.AmazonSQS.Contributors
{
	using System;
	using System.Text.Json;
	using Amazon.Runtime;
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
			AmazonSqsMessagingOptions options = context.Services.GetOptions<AmazonSqsMessagingOptions>();
			string connectionString = options.ConnectionStrings[options.ConnectionStringName];
			AmazonSqsConnectionString amazonSqsConnectionString = new AmazonSqsConnectionString(connectionString);

			MessagingOptions messagingOptions = context.Services.GetOptions<MessagingOptions>();

			configurator.UsingAmazonSqs((ctx, cfg) =>
			{
				//bool isTransactionalOutboxModuleLoaded = context.Items.ContainsKey("IsTransactionalOutboxModuleLoaded");
				//if(!isTransactionalOutboxModuleLoaded && options.InMemoryOutboxEnabled)
				//{
				//	cfg.UseInMemoryOutbox();
				//}

				if(messagingOptions.SchedulerEnabled)
				{
					cfg.UseMessageScheduler(new Uri("queue:scheduler"));
				}

				cfg.Host(amazonSqsConnectionString.Host, hostOptions =>
				{
					// optional - set the AWS Access key and Secret here if it's not configured in the environment.
					hostOptions.AccessKey(amazonSqsConnectionString.AccessKey);
					hostOptions.SecretKey(amazonSqsConnectionString.SecretKey);

					// optional - specify a scope for all queues.
					// h.Scope("dev");

					// optional - scope topics as well.
					// h.EnableScopedTopics();
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

				// Configure consume filter that sets the context on a context provider.
				cfg.UseConsumeFilter(typeof(ConsumeContextConsumeFilter<>), ctx);

				cfg.ConfigureEndpoints(ctx);
			});
		}
	}
}
