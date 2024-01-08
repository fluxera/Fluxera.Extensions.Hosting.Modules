namespace Catalog.Application.Products
{
	using System.Threading.Tasks;
	using Catalog.Application.Contracts.Products;
	using Catalog.Domain.Shared.ProductAggregate;
	using FluentResults;
	using Fluxera.Extensions.Hosting.Modules.Application.Contracts.Dtos;
	using JetBrains.Annotations;
	using MediatR;

	[UsedImplicitly]
	internal sealed class ProductApplicationService : IProductApplicationService
	{
		private readonly ISender sender;

		public ProductApplicationService(ISender sender)
		{
			this.sender = sender;
		}

		/// <inheritdoc />
		public async Task<ResultDto<ProductDto>> GetProductAsync(ProductId id)
		{
			Result<ProductDto> result = await this.sender.Send(new GetProductQuery(id));
			return result.ToResultDto();
		}

		/// <inheritdoc />
		public async Task<ResultDto<ProductDto>> AddProduct(ProductDto dto)
		{
			Result<ProductDto> result = await this.sender.Send(new AddProductCommand(dto));
			return result.ToResultDto();
		}

		/// <inheritdoc />
		public async Task<ResultDto<ProductDto[]>> GetProductsAsync()
		{
			Result<ProductDto[]> result = await this.sender.Send(new GetProductsQuery());
			return result.ToResultDto();
		}
	}
}
