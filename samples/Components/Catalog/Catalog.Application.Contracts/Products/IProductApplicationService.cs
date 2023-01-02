namespace Catalog.Application.Contracts.Products
{
	using System.Collections.Generic;
	using System.Threading.Tasks;
	using Catalog.Domain.Shared.ProductAggregate;
	using FluentResults;
	using Fluxera.Extensions.Hosting.Modules.Application.Contracts;
	using JetBrains.Annotations;

	[PublicAPI]
	public interface IProductApplicationService : IApplicationService
	{
		Task<Result<ProductDto>> GetProductAsync(ProductId id);

		Task<Result<ProductDto>> AddProduct(ProductDto dto);

		Task<Result<IReadOnlyCollection<ProductDto>>> GetProductsAsync();
	}
}
