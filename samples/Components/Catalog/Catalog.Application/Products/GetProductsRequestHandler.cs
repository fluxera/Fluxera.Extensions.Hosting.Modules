namespace Catalog.Application.Products
{
	using System;
	using System.Collections.Generic;
	using System.Threading;
	using System.Threading.Tasks;
	using AutoMapper;
	using Catalog.Application.Contracts.Products;
	using Catalog.Domain.Products;
	using Catalog.Domain.Shared.Products;
	using Fluxera.Repository;
	using JetBrains.Annotations;
	using MadEyeMatt.Results;
	using MediatR;

	[UsedImplicitly]
	internal sealed class GetProductsRequestHandler : IRequestHandler<GetProductsRequest, Result<ProductDto[]>>
	{
		private readonly IMapper mapper;
		private readonly IRepository<Product, ProductId> repository;

		public GetProductsRequestHandler(IProductRepository repository, IMapper mapper)
		{
			this.repository = repository;
			this.mapper = mapper;
		}

		/// <inheritdoc />
		public async Task<Result<ProductDto[]>> Handle(GetProductsRequest request, CancellationToken cancellationToken)
		{
			Result<ProductDto[]> result;

			try
			{
				IReadOnlyCollection<Product> entities = await this.repository.FindManyAsync(x => true, cancellationToken: cancellationToken);
				ProductDto[] dtos = this.mapper.Map<ProductDto[]>(entities);

				result = Result.Ok(dtos);
			}
			catch(Exception ex)
			{
				result = Result.Fail<ProductDto[]>(ex.Message);
			}

			return result;
		}
	}
}
