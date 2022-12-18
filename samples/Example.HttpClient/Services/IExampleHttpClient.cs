namespace Catalog.HttpClient.Services
{
	using System.Threading.Tasks;
	using Catalog.Application.Contracts.Dtos;
	using Catalog.Domain.Shared.Example;
	using Fluxera.Extensions.Http;
	using JetBrains.Annotations;

	[PublicAPI]
	public interface IExampleHttpClient : IHttpClientService
	{
		Task<ExampleDto> GetExampleAsync(ExampleId id);

		Task<ExampleDto> AddExample(ExampleDto dto);
	}
}
