namespace Fluxera.Extensions.Hosting.Modules.HttpClient
{
	using Fluxera.Extensions.Common;
	using Fluxera.Extensions.DependencyInjection;
	using Fluxera.Extensions.Hosting.Modules.Configuration;
	using Fluxera.Extensions.Hosting.Modules.HttpClient.Contributors;
	using Fluxera.Extensions.Hosting.Modules.OpenTelemetry;
	using Fluxera.Extensions.Http;
	using JetBrains.Annotations;
	using Microsoft.Extensions.DependencyInjection;
	using Microsoft.Extensions.Options;

	/// <summary>
	///     A module that enables the http client.
	/// </summary>
	[PublicAPI]
	[DependsOn(typeof(ConfigurationModule))]
	[DependsOn(typeof(OpenTelemetryModule))]
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

			// Add the contributor list.
			context.Services.TryAddObjectAccessor(new HttpClientServiceRegistrationContributorList(), ObjectAccessorLifetime.ConfigureServices);

			// Add the contributor list list.
			context.Services.TryAddObjectAccessor(new HttpClientBuilderContributorList(), ObjectAccessorLifetime.ConfigureServices);
		}

		/// <inheritdoc />
		public override void ConfigureServices(IServiceConfigurationContext context)
		{
			// Adds the hash calculator.
			context.Log("AddHashCalculator", services => services.AddHashCalculator());

			// Add named http client services.
			HttpClientServiceRegistrationContributorList contributorList = context.Services.GetObject<HttpClientServiceRegistrationContributorList>();
			HttpClientBuilderContributorList httpClientBuilderContributorList = context.Services.GetObject<HttpClientBuilderContributorList>();

			foreach(IHttpClientServiceContributor contributor in contributorList)
			{
				IHttpClientBuilder builder = contributor.AddNamedHttpClientService(context);
				builder
					.AddAuthenticateRequestHandler()
					.AddIdempotentPostRequestHandler()
					.AddContentHashRequestHandler()
					.AddContentHashResponseHandler();

				foreach(IHttpClientBuilderContributor httpClientBuilderContributor in httpClientBuilderContributorList)
				{
					httpClientBuilderContributor.Configure(builder);
				}

				// TODO: Polly, settings per service configurable.
				// https://www.hanselman.com/blog/AddingResilienceAndTransientFaultHandlingToYourNETCoreHttpClientWithPolly.aspx
				//.AddTransientHttpErrorPolicy(policyBuilder => policyBuilder.CircuitBreakerAsync(5, TimeSpan.FromSeconds(30)));
			}

			// Add a named HttpClient for the default name.
			// https://stackoverflow.com/a/57235906
			context.Log("AddHttpClient", services =>
			{
				IHttpClientBuilder builder = AddDefaultHttpClient(services);
				builder
					.AddAuthenticateRequestHandler()
					.AddIdempotentPostRequestHandler()
					.AddContentHashRequestHandler()
					.AddContentHashResponseHandler();

				foreach(IHttpClientBuilderContributor httpClientBuilderContributor in httpClientBuilderContributorList)
				{
					httpClientBuilderContributor.Configure(builder);
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
	}
}
