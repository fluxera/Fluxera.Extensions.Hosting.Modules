namespace Catalog.Domain.Products.DomainEvents
{
	using Catalog.Domain.Shared.Products;
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
