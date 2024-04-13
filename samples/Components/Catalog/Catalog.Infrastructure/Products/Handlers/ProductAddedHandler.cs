namespace Catalog.Infrastructure.Products.Handlers
{
	using Catalog.Domain.Messages.Products;
	using Catalog.Domain.Products;
	using Catalog.Domain.Shared.Products;
	using Fluxera.Extensions.Common;
	using Fluxera.Extensions.Hosting.Modules.Domain.EventHandlers;
	using JetBrains.Annotations;
	using MassTransit;
	using DomainProductAdded = Catalog.Domain.Products.DomainEvents.ProductAdded;
	using IntegrationProductAdded = Catalog.Domain.Messages.Products.ProductAdded;

	/// <summary>
	///     An event handler for bridging the <see cref="DomainProductAdded" /> domain event
	///     to the <see cref="IntegrationProductAdded" /> integration event message.
	/// </summary>
	[UsedImplicitly]
	public sealed class ProductAddedHandler : ItemAddedEventHandlerBase<Product, ProductId, Domain.Products.DomainEvents.ProductAdded, ProductAdded>
	{
		/// <summary>
		///     Initializes a new instance of the <see cref="ProductAddedHandler" /> class.
		/// </summary>
		/// <param name="publishEndpoint">The publish endpoint.</param>
		/// <param name="dateTimeOffsetProvider">The date time offset provider.</param>
		public ProductAddedHandler(
			IPublishEndpoint publishEndpoint,
			IDateTimeOffsetProvider dateTimeOffsetProvider) : base(publishEndpoint, dateTimeOffsetProvider)
		{
		}
	}
}
