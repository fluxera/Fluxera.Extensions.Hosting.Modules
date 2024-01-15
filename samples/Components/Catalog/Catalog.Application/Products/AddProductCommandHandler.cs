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
	using MediatR;

	[UsedImplicitly]
	internal sealed class AddProductCommandHandler : ICommandHandler<AddProductCommand, Result<ProductDto>>
	{
		private readonly IPublisher publisher;
		private readonly IRepository<Product, ProductId> repository;
		private readonly IUnitOfWork unitOfWork;
		private readonly IMapper mapper;

		public AddProductCommandHandler(
			IPublisher publisher,
			IProductRepository repository,
			IUnitOfWorkFactory unitOfWorkFactory,
			IMapper mapper)
		{
			this.publisher = publisher;
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

				await this.publisher.Publish(new ProductAddedEvent(product.ID), cancellationToken);

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
