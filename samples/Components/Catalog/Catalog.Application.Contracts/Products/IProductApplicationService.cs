namespace Catalog.Application.Contracts.Products
{
	using System.Threading.Tasks;
	using Catalog.Domain.Shared.Products;
	using Fluxera.Extensions.Hosting.Modules.Application.Contracts;
	using Fluxera.Extensions.Hosting.Modules.Application.Contracts.Dtos.Results;
	using JetBrains.Annotations;

	[PublicAPI]
	public interface IProductApplicationService : IApplicationService
	{
		Task<ResultDto<ProductDto>> GetProductAsync(ProductId id);

		Task<ResultDto<ProductDto>> AddProduct(ProductDto dto);

		Task<ResultDto<ProductDto[]>> GetProductsAsync();
	}
}
