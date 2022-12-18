namespace Catalog.Application.Handlers
{
	using System;
	using System.Threading;
	using System.Threading.Tasks;
	using AutoMapper;
	using Catalog.Application.Contracts.Dtos;
	using Catalog.Application.Contracts.Requests;
	using Catalog.Domain.Product;
	using Catalog.Domain.Shared.Product;
	using FluentResults;
	using Fluxera.Repository;
	using JetBrains.Annotations;
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

				result = Result.Ok(dto);
			}
			catch(Exception ex)
			{
				result = Result.Fail<ProductDto>(ex.Message);
			}

			return result;
		}
	}
}
