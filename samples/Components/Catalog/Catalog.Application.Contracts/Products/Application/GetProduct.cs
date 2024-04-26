namespace Catalog.Application.Contracts.Products.Application
{
	using Catalog.Domain.Shared.Products;
	using Fluxera.Extensions.Hosting.Modules.Application.Contracts;
	using JetBrains.Annotations;

	[PublicAPI]
	public sealed class GetProduct : IApplicationCommand<ProductDto>
	{
		public GetProduct(ProductId productId)
		{
			this.ProductId = productId;
		}

		public ProductId ProductId { get; }
	}
}
