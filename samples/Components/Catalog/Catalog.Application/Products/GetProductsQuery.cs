namespace Catalog.Application.Products
{
	using Catalog.Application.Contracts.Products;
	using Fluxera.Extensions.Hosting.Modules.Application;
	using JetBrains.Annotations;
	using MadEyeMatt.Results;

	[PublicAPI]
	public sealed class GetProductsQuery : IQuery<Result<ProductDto[]>>
	{
	}
}
