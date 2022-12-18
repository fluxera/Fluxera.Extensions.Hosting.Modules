namespace Catalog.Infrastructure.Product.Handlers
{
	using Catalog.Domain.Product;
	using Catalog.Domain.Shared.Product;
	using Fluxera.Extensions.Common;
	using Fluxera.Extensions.Hosting.Modules.Domain.EventHandlers;
	using JetBrains.Annotations;
	using MassTransit;
	using DomainProductUpdated = Catalog.Domain.Product.Events.ProductUpdated;
	using IntegrationProductUpdated = Catalog.Domain.Shared.Product.Events.ProductUpdated;

	/// <summary>
	///     An event handler for bridging the <see cref="DomainProductUpdated" /> domain event
	///     to the <see cref="IntegrationProductUpdated" /> integration event message.
	/// </summary>
	[UsedImplicitly]
	public sealed class ProductUpdatedHandler : ItemUpdatedEventHandlerBase<Product, ProductId, DomainProductUpdated, IntegrationProductUpdated>
	{
		/// <summary>
		///     Initializes a new instance of the <see cref="ProductUpdatedHandler" /> class.
		/// </summary>
		/// <param name="publishEndpoint">The publish endpoint.</param>
		/// <param name="dateTimeOffsetProvider">The date time offset provider.</param>
		public ProductUpdatedHandler(
			IPublishEndpoint publishEndpoint,
			IDateTimeOffsetProvider dateTimeOffsetProvider) : base(publishEndpoint, dateTimeOffsetProvider)
		{
		}
	}
}
