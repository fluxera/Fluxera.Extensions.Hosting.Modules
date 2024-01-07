namespace Catalog.Application.Contracts.Products
{
	using FluentResults;
	using Fluxera.Extensions.Hosting.Modules.Application.Contracts;
	using JetBrains.Annotations;

	[PublicAPI]
	public sealed class AddProductCommand : ICommand<Result<ProductDto>>
	{
		public AddProductCommand(ProductDto productDto)
		{
			this.ProductDto = productDto;
		}

		public ProductDto ProductDto { get; }
	}
}
