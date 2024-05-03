namespace Fluxera.Extensions.Hosting.Modules.Messaging
{
	using System;
	using System.Linq;
	using Fluxera.Extensions.Common;
	using Fluxera.Extensions.DependencyInjection;
	using Fluxera.Extensions.Hosting.Modules.Configuration;
	using Fluxera.Extensions.Hosting.Modules.DataManagement;
	using Fluxera.Extensions.Hosting.Modules.Messaging.Contributors;
	using Fluxera.Extensions.Hosting.Modules.Messaging.Filters;
	using Fluxera.Extensions.Hosting.Modules.OpenTelemetry;
	using Fluxera.Extensions.Hosting.Modules.Principal;
	using Fluxera.Extensions.Validation;
	using JetBrains.Annotations;
	using MassTransit;
	using Microsoft.Extensions.DependencyInjection;
	using Microsoft.Extensions.DependencyInjection.Extensions;
	using Microsoft.Extensions.Diagnostics.HealthChecks;
	using Microsoft.Extensions.Hosting;
	using Microsoft.Extensions.Logging;

	/// <summary>
	///     A module that enables MassTransit message bus.
	/// </summary>
	[PublicAPI]
	[DependsOn(typeof(PrincipalModule))]
	[DependsOn(typeof(OpenTelemetryModule))]
	[DependsOn(typeof(DataManagementModule))]
	[DependsOn(typeof(ConfigurationModule))]
	public sealed class MessagingModule : ConfigureApplicationModule
	{
		/// <inheritdoc />
		public override void PreConfigureServices(IServiceConfigurationContext context)
		{
			// Add the configure options contributor.
			context.Services.AddConfigureOptionsContributor<ConfigureOptionsContributor>();

			// Add the tracer provider contributor.
			context.Services.AddTracerProviderContributor<TracerProviderContributor>();

			// Add the consumer assembly contributor list.
			context.Log("AddObjectAccessor(ConsumersAssemblyContributorList)", services =>
			{
				services.AddObjectAccessor(new ConsumersContributorList(), ObjectAccessorLifetime.ConfigureServices);
			});

			// Add the send endpoint mapping contributor list.
			context.Log("AddObjectAccessor(SendEndpointMappingContributorList)", services =>
			{
				services.AddObjectAccessor(new SendEndpointsContributorList(), ObjectAccessorLifetime.Configure);
			});
		}

		/// <inheritdoc />
		public override void ConfigureServices(IServiceConfigurationContext context)
		{
			// Add the consume context as source for a principal.
			context.Log("AddPrincipalProvider(ConsumeContext)",
				services => services.AddPrincipalProvider<ConsumeContextPrincipalProvider>());

			// Add the hash calculator service.
			context.Log("AddHashCalculator", services => services.AddHashCalculator());

			// Add the validation service.
			context.Log("AddValidationService", services => services.AddValidation());

			// Add the principal factory service.
			context.Log("AddPrincipalFactory",
				services => services.AddTransient<IPrincipalFactory, PrincipalFactory>());

			// Add the consume context accessor.
			context.Log("AddConsumeContextAccessor", services =>
			{
				services.AddScoped(typeof(ConsumeContextConsumeFilter<>));
				services.TryAddSingleton<IConsumeContextAccessor, ConsumeContextAccessor>();
			});

			// Add services for message validation.
			context.Log("AddMessageValidation", services =>
			{
				services.AddScoped(typeof(ValidatingPublishFilter<>));
				services.AddScoped(typeof(ValidatingSendFilter<>));
				services.AddTransient<IMessageValidator, MessageValidator>();
			});

			// Add services for adding the access token to the message headers.
			context.Log("AddMessageAuthentication", services =>
			{
				services.AddScoped(typeof(AuthenticatingPublishFilter<>));
				services.AddScoped(typeof(AuthenticatingSendFilter<>));
				services.AddTransient<IMessageAuthenticator, MessageAuthenticator>();
			});

			// Add services for adding the application context to the message headers.
			context.Log("AddApplicationContextEnrichment", services =>
			{
				services.AddScoped(typeof(ApplicationContextEnrichingPublishFilter<>));
				services.AddScoped(typeof(ApplicationContextEnrichingSendFilter<>));
			});

			// Register a singleton factory for the custom endpoint name formatter.
			context.Log("AddEndpointNameFormatter", services =>
			{
				return services.AddSingleton(serviceProvider =>
				{
					string globalSubscriptionID = string.Empty;

					IHostEnvironment environment = serviceProvider.GetService<IHostEnvironment>();

					if(environment != null)
					{
						ILoggerFactory loggerFactory = serviceProvider.GetRequiredService<ILoggerFactory>();
						ILogger logger = loggerFactory.CreateLogger("EndpointNameFormatterFactory");
						IHashCalculator hashCalculator = serviceProvider.GetRequiredService<IHashCalculator>();

						globalSubscriptionID = $"{environment.EnvironmentName}-{environment.ApplicationName}";
						globalSubscriptionID = hashCalculator.ComputeHash(globalSubscriptionID);

						logger.LogServiceSubscriptionInfo(globalSubscriptionID, environment.EnvironmentName, environment.ApplicationName);
					}

					IEndpointNameFormatter endpointNameFormatter = new EndpointNameFormatter(globalSubscriptionID);
					return endpointNameFormatter;
				});
			});
		}

		/// <inheritdoc />
		public override void PostConfigureServices(IServiceConfigurationContext context)
		{
			MessagingOptions messagingOptions = context.Services.GetOptions<MessagingOptions>();

			// Add the MassTransit message bus.
			context.Log("AddMassTransit", services =>
			{
				// Get the configured consumers contributors.
				ConsumersContributorList contributorList = context.Services.GetObject<ConsumersContributorList>();

				// Get the configured transport contributor.
				ITransportContributor transportContributor = context.Services.GetObjectOrDefault<ITransportContributor>();
				if(transportContributor is null)
				{
					throw new InvalidOperationException("No transport contributor was found.");
				}

				// Get the configured outbox contributor.
				IOutboxContributor outboxContributor = context.Services.GetObjectOrDefault<IOutboxContributor>();

				services.AddMassTransit(configurator =>
				{
					if(messagingOptions.SchedulerEnabled)
					{
						configurator.AddMessageScheduler(new Uri("queue:scheduler"));
					}

					// Add (optional) transactional outbox.
					outboxContributor?.ConfigureOutbox(configurator, context);

					// Add consumers.
					foreach(IConsumersContributor contributor in contributorList)
					{
						contributor.ConfigureConsumers(configurator, context);
					}

					// Add the transport.
					transportContributor.ConfigureTransport(configurator, context);
				});

				// Rename the health check.
				services.Configure<HealthCheckServiceOptions>(options =>
				{
					HealthCheckRegistration registration = options.Registrations.Single(x => x.Name == "masstransit-bus");
					registration.Name = "MassTransit";
					registration.Tags.Add("startup");
				});
			});
		}

		/// <inheritdoc />
		public override void PostConfigure(IApplicationInitializationContext context)
		{
			SendEndpointsContributorList contributorList = context.ServiceProvider.GetObject<SendEndpointsContributorList>();

			// Register send endpoint configuration mappings.
			context.Log("MapSendEndpoints", serviceProvider =>
			{
				foreach(ISendEndpointsContributor contributor in contributorList)
				{
					ISendEndpointConfigurator configurator = new SendEndpointConfigurator(serviceProvider);
					contributor.Configure(configurator, context);
				}
			});
		}
	}
}
