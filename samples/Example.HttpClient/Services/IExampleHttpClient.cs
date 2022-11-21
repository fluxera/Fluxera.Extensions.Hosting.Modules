namespace Example.HttpClient.Services
{
	using System.Threading.Tasks;
	using Example.Application.Contracts.Dtos;
	using Example.Domain.Shared.Example;
	using Fluxera.Extensions.Http;
	using JetBrains.Annotations;

	[PublicAPI]
	public interface IExampleHttpClient : IHttpClientService
	{
		Task<ExampleDto> GetExampleAsync(ExampleId id);

		Task<ExampleDto> AddExample(ExampleDto item);
	}
}
