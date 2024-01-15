namespace Catalog.Application.Products
{
	using System;
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
	internal sealed class GetProductQueryHandler : IQueryHandler<GetProductQuery, Result<ProductDto>>
	{
		private readonly IMapper mapper;
		private readonly IRepository<Product, ProductId> repository;

		public GetProductQueryHandler(IProductRepository repository, IMapper mapper)
		{
			this.repository = repository;
			this.mapper = mapper;
		}

		/// <inheritdoc />
		public async Task<Result<ProductDto>> Handle(GetProductQuery request, CancellationToken cancellationToken)
		{
			Result<ProductDto> result;

			try
			{
				ProductId id = request.ProductId;
				Product entity = await this.repository.FindOneAsync(x => x.ID == id, cancellationToken: cancellationToken);
				ProductDto dto = this.mapper.Map<ProductDto>(entity);

				result = Result<ProductDto>.Ok(dto);
			}
			catch(Exception ex)
			{
				result = Result<ProductDto>.Fail(ex.Message);
			}

			return result;
		}
	}
}
