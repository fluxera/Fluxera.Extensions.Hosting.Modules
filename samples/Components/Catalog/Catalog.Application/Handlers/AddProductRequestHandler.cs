namespace Catalog.Application.Handlers
{
	using System;
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
	internal sealed class AddProductRequestHandler : IRequestHandler<AddProductRequest, Result<ProductDto>>
	{
		private readonly IMapper mapper;
		private readonly IRepository<Product, ProductId> repository;
		private readonly IUnitOfWork unitOfWork;

		public AddProductRequestHandler(
			IProductRepository repository,
			IUnitOfWorkFactory unitOfWorkFactory,
			IMapper mapper)
		{
			this.repository = repository;
			this.unitOfWork = unitOfWorkFactory.CreateUnitOfWork(repository.RepositoryName);
			this.mapper = mapper;
		}

		/// <inheritdoc />
		public async Task<Result<ProductDto>> Handle(AddProductRequest request, CancellationToken cancellationToken)
		{
			Result<ProductDto> result;

			try
			{
				Product product = this.mapper.Map<Product>(request.ProductDto);
				await this.repository.AddAsync(product, cancellationToken);
				await this.unitOfWork.SaveChangesAsync(cancellationToken);
				ProductDto dto = this.mapper.Map<ProductDto>(product);

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
