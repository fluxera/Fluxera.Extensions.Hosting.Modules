namespace Catalog.Domain.ProductAggregate.Events
{
	using Catalog.Domain.Shared.ProductAggregate;
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
