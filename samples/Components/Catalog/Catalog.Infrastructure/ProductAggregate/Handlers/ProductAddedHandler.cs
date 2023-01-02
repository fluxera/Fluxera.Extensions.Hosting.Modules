namespace Catalog.Infrastructure.ProductAggregate.Handlers
{
	using Catalog.Domain.ProductAggregate;
	using Catalog.Domain.Shared.ProductAggregate;
	using Fluxera.Extensions.Common;
	using Fluxera.Extensions.Hosting.Modules.Domain.EventHandlers;
	using JetBrains.Annotations;
	using MassTransit;
	using DomainProductAdded = Catalog.Domain.ProductAggregate.DomainEvents.ProductAdded;
	using IntegrationProductAdded = Catalog.Domain.Shared.ProductAggregate.Messages.ProductAdded;

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
