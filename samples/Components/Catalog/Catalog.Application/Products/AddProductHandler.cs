namespace Catalog.Application.Products
{
	using System;
	using System.Threading;
	using System.Threading.Tasks;
	using AutoMapper;
	using Catalog.Application.Contracts.Products;
	using Catalog.Application.Contracts.Products.Application;
	using Catalog.Domain.Products;
	using Catalog.Domain.Products.DomainEvents;
	using Catalog.Domain.Shared.Products;
	using Fluxera.Extensions.Hosting.Modules.Application.Handlers;
	using Fluxera.Repository;
	using Fluxera.Results;
	using JetBrains.Annotations;

	[UsedImplicitly]
	internal sealed class AddProductHandler : IApplicationCommandHandler<AddProduct, ProductDto>
	{
		private readonly IRepository<Product, ProductId> repository;
		private readonly IUnitOfWork unitOfWork;
		private readonly IMapper mapper;

		public AddProductHandler(
			IProductRepository repository,
			IUnitOfWorkFactory unitOfWorkFactory,
			IMapper mapper)
		{
			this.repository = repository;
			this.unitOfWork = unitOfWorkFactory.CreateUnitOfWork(repository.RepositoryName);
			this.mapper = mapper;
		}

		/// <inheritdoc />
		public async Task<Result<ProductDto>> HandleAsync(AddProduct command, CancellationToken cancellationToken)
		{
			Result<ProductDto> result;

			try
			{
				Product product = this.mapper.Map<Product>(command.ProductDto);
				product.RaiseDomainEvent(new ProductAdded(product.ID));

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
