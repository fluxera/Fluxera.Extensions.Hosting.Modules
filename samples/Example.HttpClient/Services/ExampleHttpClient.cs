namespace Example.HttpClient.Services
{
	using System.Net.Http;
	using System.Threading.Tasks;
	using Example.Application.Contracts.Dtos;
	using Example.Domain.Shared.Example;
	using Fluxera.Extensions.Http;
	using JetBrains.Annotations;

	[UsedImplicitly]
	internal sealed class ExampleHttpClient : HttpClientServiceBase, IExampleHttpClient
	{
		/// <inheritdoc />
		public ExampleHttpClient(string name, HttpClient httpClient, RemoteService options)
			: base(name, httpClient, options)
		{
		}

		public async Task<ExampleDto> GetExampleAsync(ExampleId id)
		{
			HttpResponseMessage response = await this.HttpClient.GetAsync($"/examples/{id}");
			return await response.Content.ReadAsAsync<ExampleDto>();
		}

		public async Task<ExampleDto> AddExample(ExampleDto item)
		{
			HttpContent content = await item.AsJsonContentAsync();
			HttpResponseMessage response = await this.HttpClient.PostAsync("/examples", content);
			return await response.Content.ReadAsAsync<ExampleDto>();
		}
	}
}
