namespace Catalog.Infrastructure.ProductAggregate.Handlers
{
	using Catalog.Domain.ProductAggregate;
	using Catalog.Domain.Shared.ProductAggregate;
	using Fluxera.Extensions.Common;
	using Fluxera.Extensions.Hosting.Modules.Domain.EventHandlers;
	using JetBrains.Annotations;
	using MassTransit;
	using DomainProductRemoved = Catalog.Domain.ProductAggregate.Events.ProductRemoved;
	using IntegrationProductRemoved = Catalog.Domain.Shared.ProductAggregate.Events.ProductRemoved;

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
