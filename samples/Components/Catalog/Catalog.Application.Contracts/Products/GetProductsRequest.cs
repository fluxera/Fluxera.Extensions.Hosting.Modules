namespace Catalog.Application.Contracts.Products
{
	using System.Collections.Generic;
	using FluentResults;
	using Fluxera.Extensions.Hosting.Modules.Application.Contracts;
	using JetBrains.Annotations;

	[PublicAPI]
	public sealed class GetProductsRequest : IQuery<Result<IReadOnlyCollection<ProductDto>>>
	{
	}
}
