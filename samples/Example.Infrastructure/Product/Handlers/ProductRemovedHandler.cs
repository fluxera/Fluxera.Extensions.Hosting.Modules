namespace Catalog.Infrastructure.Product.Handlers
{
	using Catalog.Domain.Product;
	using Catalog.Domain.Shared.Product;
	using Fluxera.Extensions.Common;
	using Fluxera.Extensions.Hosting.Modules.Domain.EventHandlers;
	using JetBrains.Annotations;
	using MassTransit;
	using DomainProductRemoved = Catalog.Domain.Product.Events.ProductRemoved;
	using IntegrationProductRemoved = Catalog.Domain.Shared.Product.Events.ProductRemoved;

	/// <summary>
	///     An event handler for bridging the <see cref="DomainProductRemoved" /> domain event
	///     to the <see cref="IntegrationProductRemoved" /> integration event message.
	/// </summary>
	[UsedImplicitly]
	public sealed class ProductRemovedHandler : ItemRemovedEventHandlerBase<Product, ProductId, DomainProductRemoved, IntegrationProductRemoved>
	{
		/// <summary>
		///     Initializes a new instance of the <see cref="ProductRemovedHandler" /> class.
		/// </summary>
		/// <param name="publishEndpoint">The publish endpoint.</param>
		/// <param name="dateTimeOffsetProvider">The date time offset provider.</param>
		public ProductRemovedHandler(
			IPublishEndpoint publishEndpoint,
			IDateTimeOffsetProvider dateTimeOffsetProvider) : base(publishEndpoint, dateTimeOffsetProvider)
		{
		}
	}
}
