namespace Catalog.Infrastructure.Products.Handlers
{
	using Catalog.Domain.Messages.Products;
	using Catalog.Domain.Products;
	using Catalog.Domain.Shared.Products;
	using Fluxera.Extensions.Common;
	using Fluxera.Extensions.Hosting.Modules.Domain.EventHandlers;
	using JetBrains.Annotations;
	using MassTransit;
	using DomainProductRemoved = Catalog.Domain.Products.DomainEvents.ProductRemoved;
	using IntegrationProductRemoved = Catalog.Domain.Messages.Products.ProductRemoved;

	/// <summary>
	///     An event handler for bridging the <see cref="DomainProductRemoved" /> domain event
	///     to the <see cref="IntegrationProductRemoved" /> integration event message.
	/// </summary>
	[UsedImplicitly]
	public sealed class ProductRemovedHandler : ItemRemovedEventHandlerBase<Product, ProductId, Domain.Products.DomainEvents.ProductRemoved, ProductRemoved>
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
