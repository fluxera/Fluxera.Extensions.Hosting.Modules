namespace Catalog.HttpClient.Services
{
	using System.Collections.Generic;
	using System.IO;
	using System.Net.Http;
	using System.Text.Json;
	using System.Text.Json.Serialization;
	using System.Threading.Tasks;
	using Catalog.Application.Contracts.Dtos;
	using Catalog.Application.Contracts.Services;
	using Catalog.Domain.Shared.ProductAggregate;
	using FluentResults;
	using Fluxera.Extensions.Http;
	using Fluxera.StronglyTypedId.SystemTextJson;
	using Fluxera.Utilities.Extensions;
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
			HttpContent content = await dto.AsJsonContentAsync();
			HttpResponseMessage response = await this.HttpClient.PostAsync("/catalog/products", content);
			ProductDto result = await response.Content.ReadAsAsync<ProductDto>();

			return Result.Ok(result);
		}

		/// <inheritdoc />
		public async Task<Result<IReadOnlyCollection<ProductDto>>> GetProductsAsync()
		{
			HttpResponseMessage response = await this.HttpClient.GetAsync("/catalog/products");

			IReadOnlyCollection<ProductDto> result;

			await using (Stream contentStream = await response.Content.ReadAsStreamAsync())
			{
				JsonSerializerOptions options = new JsonSerializerOptions
				{
					PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
					DictionaryKeyPolicy = JsonNamingPolicy.CamelCase
				};
				options.Converters.Add(new JsonStringEnumConverter());
				options.UseStronglyTypedId();
				result = (await JsonSerializer.DeserializeAsync<ProductDto[]>(contentStream, options))?.AsReadOnly();
			}

			return Result.Ok(result);
		}
	}
}
