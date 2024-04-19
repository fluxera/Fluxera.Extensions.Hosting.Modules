namespace Catalog.HttpClient.Clients
{
	using System.Net.Http;
	using System.Threading.Tasks;
	using Catalog.Application.Contracts.Products;
	using Catalog.Domain.Shared.Products;
	using Fluxera.Extensions.Hosting.Modules.Application.Contracts;
	using Fluxera.Extensions.Http;
	using JetBrains.Annotations;

	[UsedImplicitly]
	internal sealed class ProductApplicationServiceClient : HttpClientServiceBase, IProductApplicationService, IHttpClientService
	{
		/// <inheritdoc />
		public ProductApplicationServiceClient(string name, HttpClient httpClient, RemoteService options)
			: base(name, httpClient, options)
		{
		}

		/// <inheritdoc />
		public async Task<ResultDto<ProductDto>> GetProductAsync(ProductId id)
		{
			HttpResponseMessage response = await this.HttpClient.GetAsync($"/catalog/products/{id}");
			ResultDto<ProductDto> result = await response.Content.ReadAsAsync<ResultDto<ProductDto>>();

			return result;
		}

		/// <inheritdoc />
		public async Task<ResultDto<ProductDto>> AddProduct(ProductDto dto)
		{
			HttpContent content = dto.AsJsonContent();
			HttpResponseMessage response = await this.HttpClient.PostAsync("/catalog/products", content);
			ResultDto<ProductDto> result = await response.Content.ReadAsAsync<ResultDto<ProductDto>>();

			return result;
		}

		/// <inheritdoc />
		public async Task<ResultDto<ProductDto[]>> GetProductsAsync()
		{
			HttpResponseMessage response = await this.HttpClient.GetAsync("/catalog/products");
			ResultDto<ProductDto[]> result = (await response.Content.ReadAsAsync<ResultDto<ProductDto[]>>());

			return result;
		}
	}
}
