namespace Catalog.Application.Products
{
	using Catalog.Application.Contracts.Products;
	using JetBrains.Annotations;
	using MadEyeMatt.Results;
	using MediatR;

	[PublicAPI]
	public sealed class GetProductsRequest : IRequest<Result<ProductDto[]>>
	{
	}
}
