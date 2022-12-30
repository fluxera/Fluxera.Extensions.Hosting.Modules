namespace Ordering.Infrastructure.OrderAggregate.Handlers
{
	using System.Threading.Tasks;
	using Fluxera.Extensions.Hosting.Modules.Domain.EventHandlers;
	using JetBrains.Annotations;
	using MassTransit;
	using Ordering.Domain.OrderAggregate.DomainEvents;
	using Ordering.Domain.Shared.OrderAggregate.Messages;

	[UsedImplicitly]
	public sealed class OrderShippedDomainEventHandler : DomainEventHandler<OrderShippedDomainEvent>
	{
		private readonly IPublishEndpoint publishEndpoint;

		public OrderShippedDomainEventHandler(IPublishEndpoint publishEndpoint)
		{
			this.publishEndpoint = publishEndpoint;
		}

		/// <inheritdoc />
		public override async Task HandleAsync(OrderShippedDomainEvent domainEvent)
		{
			OrderShipped message = new OrderShipped(domainEvent.Order.ID, domainEvent.Order.Total);

			// Publish the event message on the message bus.
			await this.publishEndpoint.Publish(message);
		}
	}
}
