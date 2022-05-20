namespace Fluxera.Extensions.Hosting.Modules.Messaging
{
	using System;
	using Fluxera.Extensions.Common;
	using Fluxera.Extensions.DependencyInjection;
	using Fluxera.Extensions.Hosting.Modules.Configuration;
	using Fluxera.Extensions.Hosting.Modules.Messaging.Contributors;
	using Fluxera.Extensions.Hosting.Modules.Messaging.Extensions;
	using Fluxera.Extensions.Hosting.Modules.Messaging.Filters;
	using Fluxera.Extensions.Hosting.Modules.OpenTelemetry;
	using Fluxera.Extensions.Hosting.Modules.Principal;
	using JetBrains.Annotations;
	using MassTransit;
	using Microsoft.Extensions.DependencyInjection;
	using Microsoft.Extensions.DependencyInjection.Extensions;
	using Microsoft.Extensions.Hosting;
	using Microsoft.Extensions.Logging;

	/// <summary>
	///     A module that enables MassTransit message bus.
	/// </summary>
	[PublicAPI]
	[DependsOn(typeof(PrincipalModule))]
	[DependsOn(typeof(OpenTelemetryModule))]
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

						logger.LogInformation("Global Subscription ID: {GlobalSubscriptionID}, Environment={Environment}, Application={Application}",
							globalSubscriptionID, environment.EnvironmentName, environment.ApplicationName);
					}

					IEndpointNameFormatter endpointNameFormatter = new EndpointNameFormatter(globalSubscriptionID);
					return endpointNameFormatter;
				});
			});
		}

		/// <inheritdoc />
		public override void PostConfigureServices(IServiceConfigurationContext context)
		{
			// Add the MassTransit message bus.
			context.Log("AddMassTransit", services =>
			{
				ConsumersContributorList contributorList = context.Services.GetObject<ConsumersContributorList>();

				// Select a configured transport provider.
				ITransportContributor transportContributor = context.Services.GetObjectOrDefault<ITransportContributor>();
				if(transportContributor is null)
				{
					throw new InvalidOperationException("No transport contributor was found.");
				}

				services.AddMassTransit(options =>
				{
					// Add consumers.
					foreach(IConsumersContributor contributor in contributorList)
					{
						contributor.ConfigureConsumers(options, context);
					}

					// Add the transport.
					transportContributor.ConfigureTransport(options, context);
				});
			});
		}

		/// <inheritdoc />
		public override void PostConfigure(IApplicationInitializationContext context)
		{
			SendEndpointsContributorList sendEndpointsContributorList = context.ServiceProvider.GetObject<SendEndpointsContributorList>();

			// Register send endpoint configuration mappings.
			context.Log("MapSendEndpoints", serviceProvider =>
			{
				foreach(ISendEndpointsContributor sendEndpointMappingContributor in sendEndpointsContributorList)
				{
					ISendEndpointMappingConfigurator configurator = new SendEndpointMappingConfigurator(serviceProvider);
					sendEndpointMappingContributor.Configure(configurator, context);
				}
			});
		}
	}
}
