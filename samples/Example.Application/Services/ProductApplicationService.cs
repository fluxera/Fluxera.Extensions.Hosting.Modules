namespace Catalog.Application.Services
{
	using System.Threading.Tasks;
	using Catalog.Application.Contracts.Dtos;
	using Catalog.Application.Contracts.Requests;
	using Catalog.Application.Contracts.Services;
	using Catalog.Domain.Shared.Product;
	using FluentResults;
	using JetBrains.Annotations;
	using MediatR;

	[UsedImplicitly]
	internal sealed class ProductApplicationService : IProductApplicationService
	{
		private readonly ISender sender;

		public ProductApplicationService(ISender sender)
		{
			this.sender = sender;
		}

		/// <inheritdoc />
		public Task<Result<ProductDto>> GetProductAsync(ProductId id)
		{
			return this.sender.Send(new GetProductRequest(id));
		}

		/// <inheritdoc />
		public Task<Result<ProductDto>> AddProduct(ProductDto dto)
		{
			return this.sender.Send(new AddProductRequest(dto));
		}
	}
}
