namespace Catalog.Domain.Product
{
	using Catalog.Domain.Product.Events;
	using Catalog.Domain.Shared.Product;
	using Fluxera.Entity;
	using JetBrains.Annotations;

	/// <summary>
	///     An aggregate root holding the information of an example.
	/// </summary>
	/// <seealso cref="AggregateRoot{TAggregateRoot,TKey}" />
	[PublicAPI]
	public sealed class Product : AggregateRoot<Product, ProductId>
	{
		/// <summary>
		///     Gets or sets the name of the example.
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		///     Gets or sets the kind of the example.
		/// </summary>
		public ProductKind Kind { get; set; }

		/// <summary>
		///     Gets or sets the example detail.
		/// </summary>
		public ProductDetail Detail { get; set; }

		public void UpgradeProduct()
		{
			this.Kind = this.Kind switch
			{
				ProductKind.ConsumerGoods => ProductKind.Groceries,
				ProductKind.Groceries => ProductKind.LuxuryGoods,
				_ => this.Kind
			};

			this.RaiseDomainEvent(new ProductWasUpgraded(this.ID, this.Name));
		}
	}
}
