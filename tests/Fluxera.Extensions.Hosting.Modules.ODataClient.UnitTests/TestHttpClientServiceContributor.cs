namespace Fluxera.Extensions.Hosting.Modules.ODataClient.UnitTests
{
	using Fluxera.Extensions.Hosting.Modules.HttpClient;
	using Fluxera.Extensions.OData;
	using Microsoft.Extensions.DependencyInjection;
	using System.Collections.Generic;

	public class TestHttpClientServiceContributor : IHttpClientServiceContributor
	{
		/// <inheritdoc />
		public IEnumerable<IHttpClientBuilder> AddNamedHttpClientServices(IServiceConfigurationContext context)
		{
			yield return context.Services.AddODataClientService<ITestService, TestService>(
				"People",
				(ctx, _) =>
				{
					TestService testService = new TestService(ctx.Name, ctx.CollectionName, ctx.ODataClient, ctx.Options);
					return testService;
				});
		}
	}
}
