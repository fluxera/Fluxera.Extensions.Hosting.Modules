namespace Catalog.Application.Products
{
	using Catalog.Application.Contracts.Products;
	using FluentResults;
	using Fluxera.Extensions.Hosting.Modules.Application;
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
