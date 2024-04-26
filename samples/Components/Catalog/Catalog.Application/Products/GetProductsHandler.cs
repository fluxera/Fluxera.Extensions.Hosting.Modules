namespace Catalog.Application.Products
{
	using System;
	using System.Collections.Generic;
	using System.Threading;
	using System.Threading.Tasks;
	using AutoMapper;
	using Catalog.Application.Contracts.Products;
	using Catalog.Application.Contracts.Products.Application;
	using Catalog.Domain.Products;
	using Catalog.Domain.Shared.Products;
	using Fluxera.Extensions.Hosting.Modules.Application.Handlers;
	using Fluxera.Repository;
	using JetBrains.Annotations;
	using MadEyeMatt.Results;

	[UsedImplicitly]
	internal sealed class GetProductsHandler : IApplicationCommandHandler<GetProducts, ProductDto[]>
	{
		private readonly IMapper mapper;
		private readonly IRepository<Product, ProductId> repository;

		public GetProductsHandler(IProductRepository repository, IMapper mapper)
		{
			this.repository = repository;
			this.mapper = mapper;
		}

		/// <inheritdoc />
		public async Task<Result<ProductDto[]>> HandleAsync(GetProducts command, CancellationToken cancellationToken)
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
