namespace Catalog.Application.Products.Handlers
{
	using JetBrains.Annotations;
	using MassTransit;
	using System.Threading.Tasks;
	using System.Threading;
	using Catalog.Domain.Products.DomainEvents;
	using Fluxera.DomainEvents.MediatR;

	[UsedImplicitly]
	internal sealed class ProductRemovedHandler : IDomainEventHandler<ProductRemoved>
	{
		private readonly IPublishEndpoint publishEndpoint;

		public ProductRemovedHandler(IPublishEndpoint publishEndpoint)
		{
			this.publishEndpoint = publishEndpoint;
		}

		/// <inheritdoc />
		public async Task HandleAsync(ProductRemoved domainEvent, CancellationToken cancellationToken)
		{
			await this.publishEndpoint.Publish(new Contracts.Products.Integration.ProductRemoved(), cancellationToken);
		}
	}
}
