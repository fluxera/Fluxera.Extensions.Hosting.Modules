namespace Catalog.Domain.Product.Events
{
	using Catalog.Domain.Shared.Product;
	using Fluxera.Repository.DomainEvents;
	using JetBrains.Annotations;

	[PublicAPI]
	public sealed class ProductAdded : ItemAdded<Product, ProductId>
	{
		/// <inheritdoc />
		public ProductAdded(Product item)
			: base(item)
		{
		}
	}
}
