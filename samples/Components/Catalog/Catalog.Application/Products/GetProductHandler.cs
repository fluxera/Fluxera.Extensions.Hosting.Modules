namespace Catalog.Application.Products
{
	using System;
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
	internal sealed class GetProductHandler : IApplicationCommandHandler<GetProduct, ProductDto>
	{
		private readonly IMapper mapper;
		private readonly IRepository<Product, ProductId> repository;

		public GetProductHandler(IProductRepository repository, IMapper mapper)
		{
			this.repository = repository;
			this.mapper = mapper;
		}

		/// <inheritdoc />
		public async Task<Result<ProductDto>> HandleAsync(GetProduct command, CancellationToken cancellationToken)
		{
			Result<ProductDto> result;

			try
			{
				ProductId id = command.ProductId;
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
