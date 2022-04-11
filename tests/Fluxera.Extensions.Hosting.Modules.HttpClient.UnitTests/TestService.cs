namespace Fluxera.Extensions.Hosting.Modules.HttpClient.UnitTests
{
	using System.Net.Http;
	using Fluxera.Extensions.Http;

	public class TestService : HttpClientServiceBase, ITestService
	{
		/// <inheritdoc />
		public TestService(string name, HttpClient httpClient, RemoteService options)
			: base(name, httpClient, options)
		{
		}
	}
}
