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
			return null;
		}

		/// <inheritdoc />
		public async Task<ExampleDto> AddExample(ExampleDto item)
		{
			return null;
		}
	}
}
