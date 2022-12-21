namespace Catalog.Application.Contracts.Requests
{
	using System.Collections.Generic;
	using Catalog.Application.Contracts.Dtos;
	using FluentResults;
	using JetBrains.Annotations;
	using MediatR;

	[PublicAPI]
	public sealed class GetProductsRequest : IRequest<Result<IReadOnlyCollection<ProductDto>>>
	{
	}
}
