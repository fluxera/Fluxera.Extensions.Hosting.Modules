namespace Catalog.Application.Contracts.Services
{
	using System.Threading.Tasks;
	using Catalog.Application.Contracts.Dtos;
	using Catalog.Domain.Shared.ProductAggregate;
	using FluentResults;
	using Fluxera.Extensions.Hosting.Modules.Application.Contracts.Services;
	using JetBrains.Annotations;

	[PublicAPI]
	public interface IProductApplicationService : IApplicationService
	{
		Task<Result<ProductDto>> GetProductAsync(ProductId id);

		Task<Result<ProductDto>> AddProduct(ProductDto dto);
	}
}
