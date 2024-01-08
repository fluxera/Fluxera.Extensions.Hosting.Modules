namespace Catalog.Application.Products
{
	using Catalog.Application.Contracts.Products;
	using FluentResults;
	using Fluxera.Extensions.Hosting.Modules.Application;
	using JetBrains.Annotations;

	[PublicAPI]
	public sealed class GetProductsQuery : IQuery<Result<ProductDto[]>>
	{
	}
}
