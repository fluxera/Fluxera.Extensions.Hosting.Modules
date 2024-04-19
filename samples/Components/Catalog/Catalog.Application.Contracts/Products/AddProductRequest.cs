namespace Catalog.Application.Contracts.Products
{
	using JetBrains.Annotations;
	using MadEyeMatt.Results;
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
