namespace Catalog.Application.Contracts.Products
{
	using Fluxera.Extensions.Hosting.Modules.Application.Contracts;
	using JetBrains.Annotations;

	[PublicAPI]
	public sealed class AddProductCommand : ICommand<ProductDto>
	{
		public AddProductCommand(ProductDto productDto)
		{
			this.ProductDto = productDto;
		}

		public ProductDto ProductDto { get; }
	}
}
