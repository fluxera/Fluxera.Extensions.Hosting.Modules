namespace Catalog.Application.Contracts.Products
{
	using Catalog.Domain.Shared.ProductAggregate;
	using Fluxera.Extensions.Hosting.Modules.Application.Contracts;
	using JetBrains.Annotations;

	[PublicAPI]
	public sealed class ProductAddedEvent : IEvent
	{
		public ProductAddedEvent(ProductId productId)
		{
			this.ProductId = productId;
		}

		public ProductId ProductId { get; }
	}
}
