namespace Catalog.Application.Products
{
	using System;
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
	internal sealed class GetProductRequestHandler : IRequestHandler<GetProductRequest, Result<ProductDto>>
	{
		private readonly IMapper mapper;
		private readonly IRepository<Product, ProductId> repository;

		public GetProductRequestHandler(IProductRepository repository, IMapper mapper)
		{
			this.repository = repository;
			this.mapper = mapper;
		}

		/// <inheritdoc />
		public async Task<Result<ProductDto>> Handle(GetProductRequest request, CancellationToken cancellationToken)
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
