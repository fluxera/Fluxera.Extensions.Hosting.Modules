﻿namespace Fluxera.Extensions.Hosting.Modules.HttpClient
{
	using Fluxera.Extensions.Common;
	using Fluxera.Extensions.DependencyInjection;
	using Fluxera.Extensions.Hosting.Modules.Configuration;
	using Fluxera.Extensions.Hosting.Modules.HttpClient.Contributors;
	using Fluxera.Extensions.Hosting.Modules.OpenTelemetry;
	using Fluxera.Extensions.Http;
	using Fluxera.Utilities.Extensions;
	using JetBrains.Annotations;
	using Microsoft.Extensions.DependencyInjection;
	using Microsoft.Extensions.Options;

	/// <summary>
	///     A module that enables the http client.
	/// </summary>
	[PublicAPI]
	[DependsOn(typeof(OpenTelemetryModule))]
	[DependsOn(typeof(ConfigurationModule))]
	public sealed class HttpClientModule : ConfigureServicesModule
	{
		/// <inheritdoc />
		public override void PreConfigureServices(IServiceConfigurationContext context)
		{
			// Add the configure options contributor.
			context.Services.AddConfigureOptionsContributor<ConfigureOptionsContributor>();

			// Add the tracer provider contributor.
			context.Services.AddTracerProviderContributor<TracerProviderContributor>();

			// Add the meter provider contributor.
			context.Services.AddMeterProviderContributor<MeterProviderContributor>();

			// Add the http client service contributor list.
			context.Services.TryAddObjectAccessor(new HttpClientServiceContributorList(), ObjectAccessorLifetime.ConfigureServices);

			// Add the http client builder contributor list.
			context.Services.TryAddObjectAccessor(new HttpClientBuilderContributorList(), ObjectAccessorLifetime.ConfigureServices);
		}

		/// <inheritdoc />
		public override void ConfigureServices(IServiceConfigurationContext context)
		{
			// Adds the hash calculator.
			context.Log("AddHashCalculator", services => services.AddHashCalculator());

			// Add named http client services.
			HttpClientServiceContributorList serviceContributorList = context.Services.GetObject<HttpClientServiceContributorList>();
			HttpClientBuilderContributorList builderContributorList = context.Services.GetObject<HttpClientBuilderContributorList>();

			HttpClientOptions httpClientOptions = context.Services.GetOptions<HttpClientOptions>();
			
			foreach(IHttpClientServiceContributor serviceContributor in serviceContributorList)
			{
				foreach(IHttpClientBuilder builder in serviceContributor.AddNamedHttpClientServices(context))
				{
					RemoteService remoteService = httpClientOptions.RemoteServices.GetOrDefault(builder.Name);
					foreach(IHttpClientBuilderContributor builderContributor in builderContributorList)
					{
						builderContributor.ConfigureHttpClientBuilder(builder, remoteService, context);
					}
				}
			}

			// Add a named HttpClient for the default name.
			// https://stackoverflow.com/a/57235906
			context.Log("AddHttpClient", services =>
			{
				IHttpClientBuilder builder = AddDefaultHttpClient(services);
				foreach(IHttpClientBuilderContributor builderContributor in builderContributorList)
				{
					builderContributor.ConfigureHttpClientBuilder(builder, null, context);
				}
			});
		}

		private static IHttpClientBuilder AddDefaultHttpClient(IServiceCollection services)
		{
			IHttpClientBuilder builder = services.AddHttpClient(Options.DefaultName, (serviceProvider, httpClient) =>
			{
				// Use the optional base address provider for the default http client.
				IBaseAddressProvider baseAddressProvider = serviceProvider.GetService<IBaseAddressProvider>();
				if(baseAddressProvider != null)
				{
					httpClient.BaseAddress = baseAddressProvider.BaseAddress;
				}
			});

			return builder;
		}

		//private static void AddMessageHandlers(IHttpClientBuilder builder, RemoteService remoteService = default)
		//{
		//	builder
		//		.AddAuthenticateRequestHandler()
		//		.AddIdempotentPostRequestHandler()
		//		.AddContentHashRequestHandler()
		//		.AddContentHashResponseHandler();

		//	// TODO: Polly, settings per service configurable.
		//	// https://www.hanselman.com/blog/AddingResilienceAndTransientFaultHandlingToYourNETCoreHttpClientWithPolly.aspx
		//	//.AddTransientHttpErrorPolicy(policyBuilder => policyBuilder.CircuitBreakerAsync(5, TimeSpan.FromSeconds(30)));
		//}
	}
}
