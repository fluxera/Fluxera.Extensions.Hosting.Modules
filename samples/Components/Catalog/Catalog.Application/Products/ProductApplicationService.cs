namespace Catalog.Application.Products
{
	using System.Collections.Generic;
	using System.Threading.Tasks;
	using Catalog.Application.Contracts.Products;
	using Catalog.Domain.Shared.ProductAggregate;
	using FluentResults;
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
		public Task<Result<ProductDto>> GetProductAsync(ProductId id)
		{
			return this.sender.Send(new GetProductQuery(id));
		}

		/// <inheritdoc />
		public Task<Result<ProductDto>> AddProduct(ProductDto dto)
		{
			return this.sender.Send(new AddProductCommand(dto));
		}

		/// <inheritdoc />
		public Task<Result<IReadOnlyCollection<ProductDto>>> GetProductsAsync()
		{
			return this.sender.Send(new GetProductsRequest());
		}
	}
}
