namespace Catalog.Application.Products
{
	using Catalog.Application.Contracts.Products;
	using Catalog.Domain.Shared.ProductAggregate;
	using FluentResults;
	using Fluxera.Extensions.Hosting.Modules.Application;
	using JetBrains.Annotations;

	[PublicAPI]
	public sealed class GetProductQuery : IQuery<Result<ProductDto>>
	{
		public GetProductQuery(ProductId productId)
		{
			this.ProductId = productId;
		}

		public ProductId ProductId { get; }
	}
}
