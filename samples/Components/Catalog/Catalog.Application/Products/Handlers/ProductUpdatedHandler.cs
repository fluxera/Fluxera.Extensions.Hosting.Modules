namespace Catalog.Application.Products.Handlers
{
	using System.Threading;
	using System.Threading.Tasks;
	using Catalog.Domain.Products.DomainEvents;
	using Fluxera.Entity.DomainEvents;
	using JetBrains.Annotations;
	using MassTransit;

	[UsedImplicitly]
	internal sealed class ProductUpdatedHandler : IDomainEventHandler<ProductUpdated>
	{
		private readonly IPublishEndpoint publishEndpoint;

		public ProductUpdatedHandler(IPublishEndpoint publishEndpoint)
		{
			this.publishEndpoint = publishEndpoint;
		}

		/// <inheritdoc />
		public async Task HandleAsync(ProductUpdated domainEvent, CancellationToken cancellationToken)
		{
			await this.publishEndpoint.Publish(new Contracts.Products.Integration.ProductUpdated(), cancellationToken);
		}
	}
}
