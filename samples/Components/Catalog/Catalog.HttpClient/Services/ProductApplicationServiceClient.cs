namespace Catalog.HttpClient.Services
{
	using System.Collections.Generic;
	using System.Net.Http;
	using System.Threading.Tasks;
	using Catalog.Application.Contracts.Products;
	using Catalog.Domain.Shared.ProductAggregate;
	using FluentResults;
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
		public async Task<Result<ProductDto>> GetProductAsync(ProductId id)
		{
			HttpResponseMessage response = await this.HttpClient.GetAsync($"/catalog/products/{id}");
			ProductDto result = await response.Content.ReadAsAsync<ProductDto>();

			return Result.Ok(result);
		}

		/// <inheritdoc />
		public async Task<Result<ProductDto>> AddProduct(ProductDto dto)
		{
			HttpContent content = dto.AsJsonContent();
			HttpResponseMessage response = await this.HttpClient.PostAsync("/catalog/products", content);
			ProductDto result = await response.Content.ReadAsAsync<ProductDto>();

			return Result.Ok(result);
		}

		/// <inheritdoc />
		public async Task<Result<IReadOnlyCollection<ProductDto>>> GetProductsAsync()
		{
			HttpResponseMessage response = await this.HttpClient.GetAsync("/catalog/products");
			IReadOnlyCollection<ProductDto> result = (await response.Content.ReadAsAsync<ProductDto[]>()).AsReadOnly();

			return Result.Ok(result);
		}
	}
}
