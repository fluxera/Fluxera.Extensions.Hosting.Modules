namespace Catalog.Application.Contracts.Products.Application
{
	using Fluxera.Extensions.Hosting.Modules.Application.Contracts;
	using JetBrains.Annotations;

	[PublicAPI]
	public sealed class AddProduct : IApplicationCommand<ProductDto>
	{
		public AddProduct(ProductDto productDto)
		{
			this.ProductDto = productDto;
		}

		public ProductDto ProductDto { get; }
	}
}
