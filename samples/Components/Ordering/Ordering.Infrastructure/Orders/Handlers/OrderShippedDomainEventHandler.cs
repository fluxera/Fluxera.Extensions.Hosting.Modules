namespace Ordering.Infrastructure.Orders.Handlers
{
	using System.Threading;
	using System.Threading.Tasks;
	using Fluxera.Entity.DomainEvents;
	using JetBrains.Annotations;
	using MassTransit;
	using Ordering.Application.Contracts.Orders.Messages;
	using Ordering.Domain.Orders.DomainEvents;

	[UsedImplicitly]
	public sealed class OrderShippedDomainEventHandler : IDomainEventHandler<OrderShippedDomainEvent>
	{
		private readonly IPublishEndpoint publishEndpoint;

		public OrderShippedDomainEventHandler(IPublishEndpoint publishEndpoint)
		{
			this.publishEndpoint = publishEndpoint;
		}

		/// <inheritdoc />
		public async Task HandleAsync(OrderShippedDomainEvent domainEvent, CancellationToken cancellationToken)
		{
			OrderShipped message = new OrderShipped(domainEvent.Order.ID, domainEvent.Order.Total);

			// Publish the event message on the message bus.
			await this.publishEndpoint.Publish(message, cancellationToken);
		}
	}
}
