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
	internal sealed class AddProductRequestHandler : IRequestHandler<AddProductRequest, Result<ProductDto>>
	{
		private readonly IRepository<Product, ProductId> repository;
		private readonly IUnitOfWork unitOfWork;
		private readonly IMapper mapper;

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
