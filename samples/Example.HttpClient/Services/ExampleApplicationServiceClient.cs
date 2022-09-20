namespace Example.HttpClient.Services
{
	using System.Net.Http;
	using System.Threading.Tasks;
	using Example.Application.Contracts.Dtos;
	using Example.Application.Contracts.Services;
	using Example.Domain.Shared.ExampleAggregate.Model;
	using Fluxera.Extensions.Http;
	using JetBrains.Annotations;

	[UsedImplicitly]
	internal sealed class ExampleApplicationServiceClient : HttpClientServiceBase, IExampleApplicationService
	{
		/// <inheritdoc />
		public ExampleApplicationServiceClient(string name, HttpClient httpClient, RemoteService options)
			: base(name, httpClient, options)
		{
		}

		/// <inheritdoc />
		public async Task<ExampleDto> GetExampleAsync(ExampleId id)
		{
			HttpResponseMessage response = await this.HttpClient.GetAsync($"/examples/{id}");
			return await response.Content.ReadAsAsync<ExampleDto>();
		}

		/// <inheritdoc />
		public async Task<ExampleDto> AddExample(ExampleDto item)
		{
			HttpContent content = await item.AsJsonContentAsync();
			HttpResponseMessage response = await this.HttpClient.PostAsync("/examples", content);
			return await response.Content.ReadAsAsync<ExampleDto>();
		}
	}
}
