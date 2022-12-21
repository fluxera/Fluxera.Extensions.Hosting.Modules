namespace Catalog.Application.Handlers
{
	using System;
	using System.Collections.Generic;
	using System.Threading;
	using System.Threading.Tasks;
	using AutoMapper;
	using Catalog.Application.Contracts.Dtos;
	using Catalog.Application.Contracts.Requests;
	using Catalog.Domain.ProductAggregate;
	using Catalog.Domain.Shared.ProductAggregate;
	using FluentResults;
	using Fluxera.Repository;
	using JetBrains.Annotations;
	using MediatR;

	[UsedImplicitly]
	internal sealed class GetProductsRequestHandler : IRequestHandler<GetProductsRequest, Result<IReadOnlyCollection<ProductDto>>>
	{
		private readonly IMapper mapper;
		private readonly IRepository<Product, ProductId> repository;

		public GetProductsRequestHandler(IProductRepository repository, IMapper mapper)
		{
			this.repository = repository;
			this.mapper = mapper;
		}

		/// <inheritdoc />
		public async Task<Result<IReadOnlyCollection<ProductDto>>> Handle(GetProductsRequest request, CancellationToken cancellationToken)
		{
			Result<IReadOnlyCollection<ProductDto>> result;

			try
			{
				IReadOnlyCollection<Product> entities = await this.repository.FindManyAsync(x => true, cancellationToken: cancellationToken);
				IReadOnlyCollection<ProductDto> dtos = this.mapper.Map<IReadOnlyCollection<ProductDto>>(entities);

				result = Result.Ok(dtos);
			}
			catch(Exception ex)
			{
				result = Result.Fail<IReadOnlyCollection<ProductDto>>(ex.Message);
			}

			return result;
		}
	}
}
