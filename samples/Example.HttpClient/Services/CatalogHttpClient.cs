namespace Catalog.HttpClient.Services
{
	using System.Net.Http;
	using System.Threading.Tasks;
	using Catalog.Application.Contracts.Dtos;
	using Catalog.Domain.Shared.Product;
	using Fluxera.Extensions.Http;
	using JetBrains.Annotations;

	[UsedImplicitly]
	internal sealed class CatalogHttpClient : HttpClientServiceBase, ICatalogHttpClient
	{
		/// <inheritdoc />
		public CatalogHttpClient(string name, HttpClient httpClient, RemoteService options)
			: base(name, httpClient, options)
		{
		}

		public async Task<ProductDto> GetProductAsync(ProductId id)
		{
			HttpResponseMessage response = await this.HttpClient.GetAsync($"/catalog/products/{id}");
			return await response.Content.ReadAsAsync<ProductDto>();
		}

		public async Task<ProductDto> AddProduct(ProductDto dto)
		{
			HttpContent content = await dto.AsJsonContentAsync();
			HttpResponseMessage response = await this.HttpClient.PostAsync("/catalog/products", content);
			return await response.Content.ReadAsAsync<ProductDto>();
		}
	}
}
