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
	internal sealed class AddProductCommandHandler : ICommandHandler<AddProductCommand, Result<ProductDto>>
	{
		private readonly IRepository<Product, ProductId> repository;
		private readonly IUnitOfWork unitOfWork;
		private readonly IMapper mapper;

		public AddProductCommandHandler(
			IProductRepository repository,
			IUnitOfWorkFactory unitOfWorkFactory,
			IMapper mapper)
		{
			this.repository = repository;
			this.unitOfWork = unitOfWorkFactory.CreateUnitOfWork(repository.RepositoryName);
			this.mapper = mapper;
		}

		/// <inheritdoc />
		public async Task<Result<ProductDto>> Handle(AddProductCommand request, CancellationToken cancellationToken)
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
