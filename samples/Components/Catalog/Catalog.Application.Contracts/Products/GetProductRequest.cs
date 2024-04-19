namespace Catalog.Application.Contracts.Products
{
	using Catalog.Domain.Shared.Products;
	using JetBrains.Annotations;
	using MadEyeMatt.Results;
	using MediatR;

	[PublicAPI]
	public sealed class GetProductRequest : IRequest<Result<ProductDto>>
	{
		public GetProductRequest(ProductId productId)
		{
			this.ProductId = productId;
		}

		public ProductId ProductId { get; }
	}
}
