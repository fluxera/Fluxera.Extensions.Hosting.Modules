namespace Catalog.Application.Contracts.Products
{
	using System.Threading.Tasks;
	using Catalog.Domain.Shared.ProductAggregate;
	using Fluxera.Extensions.Hosting.Modules.Application.Contracts;
	using Fluxera.Extensions.Hosting.Modules.Application.Contracts.Dtos;
	using JetBrains.Annotations;

	[PublicAPI]
	public interface IProductApplicationService : IApplicationService
	{
		Task<ResultDto<ProductDto>> GetProductAsync(ProductId id);

		Task<ResultDto<ProductDto>> AddProduct(ProductDto dto);

		Task<ResultDto<ProductDto[]>> GetProductsAsync();
	}
}
