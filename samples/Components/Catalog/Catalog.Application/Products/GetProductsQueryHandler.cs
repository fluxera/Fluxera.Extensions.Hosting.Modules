namespace Catalog.Application.Products
{
	using System;
	using System.Collections.Generic;
	using System.Threading;
	using System.Threading.Tasks;
	using AutoMapper;
	using Catalog.Application.Contracts.Products;
	using Catalog.Domain.ProductAggregate;
	using Catalog.Domain.Shared.ProductAggregate;
	using Fluxera.Extensions.Hosting.Modules.Application;
	using Fluxera.Repository;
	using JetBrains.Annotations;
	using MadEyeMatt.Results;

	[UsedImplicitly]
	internal sealed class GetProductsQueryHandler : IQueryHandler<GetProductsQuery, Result<ProductDto[]>>
	{
		private readonly IMapper mapper;
		private readonly IRepository<Product, ProductId> repository;

		public GetProductsQueryHandler(IProductRepository repository, IMapper mapper)
		{
			this.repository = repository;
			this.mapper = mapper;
		}

		/// <inheritdoc />
		public async Task<Result<ProductDto[]>> Handle(GetProductsQuery query, CancellationToken cancellationToken)
		{
			Result<ProductDto[]> result;

			try
			{
				IReadOnlyCollection<Product> entities = await this.repository.FindManyAsync(x => true, cancellationToken: cancellationToken);
				ProductDto[] dtos = this.mapper.Map<ProductDto[]>(entities);

				result = Result<ProductDto[]>.Ok(dtos);
			}
			catch(Exception ex)
			{
				result = Result<ProductDto[]>.Fail(ex.Message);
			}

			return result;
		}
	}
}
