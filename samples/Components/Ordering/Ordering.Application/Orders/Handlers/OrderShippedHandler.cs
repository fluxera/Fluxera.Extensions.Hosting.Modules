namespace Ordering.Application.Orders.Handlers
{
	using System.Threading;
	using System.Threading.Tasks;
	using Fluxera.Entity.DomainEvents;
	using JetBrains.Annotations;
	using MassTransit;
	using Ordering.Domain.Orders.DomainEvents;

	[UsedImplicitly]
	public sealed class OrderShippedHandler : IDomainEventHandler<OrderShipped>
	{
		private readonly IPublishEndpoint publishEndpoint;

		public OrderShippedHandler(IPublishEndpoint publishEndpoint)
		{
			this.publishEndpoint = publishEndpoint;
		}

		/// <inheritdoc />
		public async Task HandleAsync(OrderShipped domainEvent, CancellationToken cancellationToken)
		{
			// Publish the event message on the message bus.
			await this.publishEndpoint.Publish(new Contracts.Orders.Messages.OrderShipped(domainEvent.Order.ID, domainEvent.Order.Total), cancellationToken);
		}
	}
}
