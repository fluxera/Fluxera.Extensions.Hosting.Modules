namespace Catalog.Application.Products
{
	using System.Threading.Tasks;
	using Catalog.Application.Contracts.Products;
	using Catalog.Domain.Shared.Products;
	using Fluxera.Extensions.Hosting.Modules.Application;
	using Fluxera.Extensions.Hosting.Modules.Application.Contracts;
	using JetBrains.Annotations;
	using MadEyeMatt.Results;
	using MediatR;

	[UsedImplicitly]
	internal sealed class ProductApplicationService : IProductApplicationService
	{
		private readonly IMediator mediator;

		public ProductApplicationService(IMediator mediator)
		{
			this.mediator = mediator;
		}

		/// <inheritdoc />
		public async Task<ResultDto<ProductDto>> GetProductAsync(ProductId id)
		{
			Result<ProductDto> result = await this.mediator.Send(new GetProductRequest(id));
			return result.ToResultDto();
		}

		/// <inheritdoc />
		public async Task<ResultDto<ProductDto>> AddProduct(ProductDto dto)
		{
			Result<ProductDto> result = await this.mediator.Send(new AddProductRequest(dto));
			return result.ToResultDto();
		}

		/// <inheritdoc />
		public async Task<ResultDto<ProductDto[]>> GetProductsAsync()
		{
			Result<ProductDto[]> result = await this.mediator.Send(new GetProductsRequest());
			return result.ToResultDto();
		}
	}
}
