namespace Fluxera.Extensions.Hosting.Modules.HttpClient.UnitTests
{
	using Fluxera.Extensions.Http;
	using Microsoft.Extensions.DependencyInjection;

	public class TestHttpClientServiceContributor : IHttpClientServiceContributor
	{
		/// <inheritdoc />
		public IHttpClientBuilder AddNamedHttpClientService(IServiceConfigurationContext context)
		{
			IHttpClientBuilder httpClientBuilder = context.Services.AddHttpClientService<ITestService, TestService>(
				ctx =>
				{
					TestService testService = new TestService(ctx.Name, ctx.HttpClient, ctx.Options);
					return testService;
				});

			return httpClientBuilder;
		}
	}
}
