namespace Catalog.Domain.Product.Events
{
	using Catalog.Domain.Shared.Product;
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
