namespace Catalog.Domain.Product.Events
{
	using Catalog.Domain.Shared.Product;
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
