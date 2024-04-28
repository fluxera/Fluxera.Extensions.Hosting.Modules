namespace Catalog.Application.Products.Handlers
{
	using System.Threading;
	using System.Threading.Tasks;
	using Catalog.Domain.Products.DomainEvents;
	using Fluxera.DomainEvents.MediatR;
	using JetBrains.Annotations;
	using MassTransit;

	[UsedImplicitly]
	internal sealed class ProductAddedHandler : IDomainEventHandler<ProductAdded>
	{
		private readonly IPublishEndpoint publishEndpoint;

		public ProductAddedHandler(IPublishEndpoint publishEndpoint)
		{
			this.publishEndpoint = publishEndpoint;
		}

		/// <inheritdoc />
		public async Task HandleAsync(ProductAdded domainEvent, CancellationToken cancellationToken)
		{
			await this.publishEndpoint.Publish(new Contracts.Products.Integration.ProductAdded(), cancellationToken);
		}
	}
}
