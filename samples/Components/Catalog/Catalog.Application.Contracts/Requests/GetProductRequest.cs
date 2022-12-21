namespace Catalog.Application.Contracts.Requests
{
	using Catalog.Application.Contracts.Dtos;
	using Catalog.Domain.Shared.ProductAggregate;
	using FluentResults;
	using JetBrains.Annotations;
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
