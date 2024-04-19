namespace Catalog.Application.Products
{
	using System.Threading.Tasks;
	using AutoMapper;
	using Catalog.Application.Contracts.Products;
	using Catalog.Domain.Shared.Products;
	using Fluxera.Extensions.Hosting.Modules.Application.Contracts;
	using JetBrains.Annotations;
	using MadEyeMatt.Results;
	using MediatR;

	[UsedImplicitly]
	internal sealed class ProductApplicationService : IProductApplicationService
	{
		private readonly ISender sender;
		private readonly IMapper mapper;

		public ProductApplicationService(ISender sender, IMapper mapper)
		{
			this.sender = sender;
			this.mapper = mapper;
		}

		/// <inheritdoc />
		public async Task<ResultDto<ProductDto>> GetProductAsync(ProductId id)
		{
			Result<ProductDto> result = await this.sender.Send(new GetProductRequest(id));
			return this.mapper.Map<ResultDto<ProductDto>>(result);
		}

		/// <inheritdoc />
		public async Task<ResultDto<ProductDto>> AddProduct(ProductDto dto)
		{
			Result<ProductDto> result = await this.sender.Send(new AddProductRequest(dto));
			return this.mapper.Map<ResultDto<ProductDto>>(result);
		}

		/// <inheritdoc />
		public async Task<ResultDto<ProductDto[]>> GetProductsAsync()
		{
			Result<ProductDto[]> result = await this.sender.Send(new GetProductsRequest());
			return this.mapper.Map<ResultDto<ProductDto[]>>(result);
		}
	}
}
