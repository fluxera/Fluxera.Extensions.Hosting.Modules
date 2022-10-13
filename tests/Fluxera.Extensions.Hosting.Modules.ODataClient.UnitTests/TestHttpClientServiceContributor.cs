namespace Fluxera.Extensions.Hosting.Modules.ODataClient.UnitTests
{
	using Fluxera.Extensions.Hosting.Modules.HttpClient;
	using Fluxera.Extensions.OData;
	using Microsoft.Extensions.DependencyInjection;

	public class TestHttpClientServiceContributor : IHttpClientServiceContributor
	{
		/// <inheritdoc />
		public IHttpClientBuilder AddNamedHttpClientServices(IServiceConfigurationContext context)
		{
			IHttpClientBuilder httpClientBuilder = context.Services.AddODataClientService<ITestService, TestService>(
				"People",
				(ctx, _) =>
				{
					TestService testService = new TestService(ctx.Name, ctx.CollectionName, ctx.ODataClient, ctx.Options);
					return testService;
				});

			return httpClientBuilder;
		}
	}
}
