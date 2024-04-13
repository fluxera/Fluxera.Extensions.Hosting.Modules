namespace Ordering.Infrastructure.Orders.Handlers
{
	using System.Threading.Tasks;
	using Fluxera.Extensions.Hosting.Modules.Domain.EventHandlers;
	using JetBrains.Annotations;
	using MassTransit;
	using Ordering.Domain.Messages.Orders;
	using Ordering.Domain.Orders.DomainEvents;

	[UsedImplicitly]
	public sealed class OrderSubmittedDomainEventHandler : DomainEventHandler<OrderSubmittedDomainEvent>
	{
		private readonly IPublishEndpoint publishEndpoint;

		public OrderSubmittedDomainEventHandler(IPublishEndpoint publishEndpoint)
		{
			this.publishEndpoint = publishEndpoint;
		}

		/// <inheritdoc />
		public override async Task HandleAsync(OrderSubmittedDomainEvent domainEvent)
		{
			OrderSubmitted message = new OrderSubmitted(domainEvent.Order.ID, domainEvent.Order.Total);

			// Publish the event message on the message bus.
			await this.publishEndpoint.Publish(message);
		}
	}
}
