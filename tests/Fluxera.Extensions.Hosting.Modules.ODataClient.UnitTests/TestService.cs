namespace Fluxera.Extensions.Hosting.Modules.ODataClient.UnitTests
{
	using System.Net.Http;
	using Fluxera.Extensions.Http;
	using Fluxera.Extensions.OData;
	using Simple.OData.Client;

	public class TestService : ODataClientServiceBase<Person, string>, ITestService
	{
		/// <inheritdoc />
		public TestService(string name, string collectionName, HttpClient httpClient, IODataClient oDataClient, RemoteService options)
			: base(name, collectionName, httpClient, oDataClient, options)
		{
		}
	}
}
