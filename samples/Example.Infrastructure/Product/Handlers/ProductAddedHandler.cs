namespace Catalog.Infrastructure.Product.Handlers
{
	using Catalog.Domain.Product;
	using Catalog.Domain.Shared.Product;
	using Fluxera.Extensions.Common;
	using Fluxera.Extensions.Hosting.Modules.Domain.EventHandlers;
	using JetBrains.Annotations;
	using MassTransit;
	using DomainProductAdded = Catalog.Domain.Product.Events.ProductAdded;
	using IntegrationProductAdded = Catalog.Domain.Shared.Product.Events.ProductAdded;

	/// <summary>
	///     An event handler for bridging the <see cref="DomainProductAdded" /> domain event
	///     to the <see cref="IntegrationProductAdded" /> integration event message.
	/// </summary>
	[UsedImplicitly]
	public sealed class ProductAddedHandler : ItemAddedEventHandlerBase<Product, ProductId, DomainProductAdded, IntegrationProductAdded>
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
