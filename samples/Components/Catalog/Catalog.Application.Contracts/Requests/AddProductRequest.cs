namespace Catalog.Application.Contracts.Requests
{
	using Catalog.Application.Contracts.Dtos;
	using FluentResults;
	using JetBrains.Annotations;
	using MediatR;

	[PublicAPI]
	public sealed class AddProductRequest : IRequest<Result<ProductDto>>
	{
		public AddProductRequest(ProductDto productDto)
		{
			this.ProductDto = productDto;
		}

		public ProductDto ProductDto { get; }
	}
}
