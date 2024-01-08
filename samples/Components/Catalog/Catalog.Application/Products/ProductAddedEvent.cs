namespace Catalog.Application.Products
{
	using Catalog.Domain.Shared.ProductAggregate;
	using Fluxera.Extensions.Hosting.Modules.Application;
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
