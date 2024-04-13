namespace Catalog.Domain.Products.DomainEvents
{
	using Catalog.Domain.Shared.Products;
	using Fluxera.Repository.DomainEvents;
	using JetBrains.Annotations;

	[PublicAPI]
	public sealed class ProductRemoved : ItemRemoved<Product, ProductId>
	{
		/// <inheritdoc />
		public ProductRemoved(ProductId id, Product item)
			: base(id, item)
		{
		}
	}
}
