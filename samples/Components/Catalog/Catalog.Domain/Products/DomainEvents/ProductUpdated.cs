namespace Catalog.Domain.Products.DomainEvents
{
	using Catalog.Domain.Shared.Products;
	using Fluxera.Repository.DomainEvents;
	using JetBrains.Annotations;

	[PublicAPI]
	public sealed class ProductUpdated : ItemUpdated<Product, ProductId>
	{
		/// <inheritdoc />
		public ProductUpdated(Product beforeUpdateItem, Product afterUpdateItem)
			: base(beforeUpdateItem, afterUpdateItem)
		{
		}
	}
}
