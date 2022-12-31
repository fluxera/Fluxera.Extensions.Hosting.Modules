namespace Catalog.Application.Contracts.Products
{
	using System.Collections.Generic;
	using FluentResults;
	using Fluxera.Extensions.Hosting.Modules.Application.Contracts;
	using JetBrains.Annotations;

	[PublicAPI]
	public sealed class GetProductsQuery : IQuery<Result<IReadOnlyCollection<ProductDto>>>
	{
	}
}
