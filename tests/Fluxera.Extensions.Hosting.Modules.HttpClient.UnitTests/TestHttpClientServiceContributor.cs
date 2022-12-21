namespace Fluxera.Extensions.Hosting.Modules.HttpClient.UnitTests
{
	using Fluxera.Extensions.Http;
	using Microsoft.Extensions.DependencyInjection;
	using System.Collections.Generic;

	public class TestHttpClientServiceContributor : IHttpClientServiceContributor
	{
		/// <inheritdoc />
		public IEnumerable<IHttpClientBuilder> AddNamedHttpClientServices(IServiceConfigurationContext context)
		{
			yield return context.Services.AddHttpClientService<ITestService, TestService>(
				(ctx, _) =>
				{
					TestService testService = new TestService(ctx.Name, ctx.HttpClient, ctx.Options);
					return testService;
				});
		}
	}
}
