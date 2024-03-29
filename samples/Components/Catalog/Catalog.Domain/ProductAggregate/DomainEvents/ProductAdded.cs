﻿namespace Catalog.Domain.ProductAggregate.DomainEvents
{
	using Catalog.Domain.Shared.ProductAggregate;
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
