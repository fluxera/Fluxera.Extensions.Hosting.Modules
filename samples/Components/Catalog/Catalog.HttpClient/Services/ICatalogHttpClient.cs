namespace Catalog.HttpClient.Services
{
	using System.Threading.Tasks;
	using Catalog.Application.Contracts.Dtos;
	using Catalog.Domain.Shared.ProductAggregate;
	using Fluxera.Extensions.Http;
	using JetBrains.Annotations;

	[PublicAPI]
	public interface ICatalogHttpClient : IHttpClientService
	{
		Task<ProductDto> GetProductAsync(ProductId id);

		Task<ProductDto> AddProduct(ProductDto dto);
	}
}
