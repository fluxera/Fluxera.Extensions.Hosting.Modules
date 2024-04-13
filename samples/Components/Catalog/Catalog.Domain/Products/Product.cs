namespace Catalog.Domain.Products
{
	using Catalog.Domain.Shared.Products;
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
		///     Gets or sets the name.
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		///     Gets or sets the description.
		/// </summary>
		public string Description { get; set; }

		/// <summary>
		///     Gets or sets the price.
		/// </summary>
		public decimal Price { get; set; }
	}
}
