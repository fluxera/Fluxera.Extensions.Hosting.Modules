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
	public sealed class OrderSubmittedDomainEventHandler : IDomainEventHandler<OrderSubmittedDomainEvent>
	{
		private readonly IPublishEndpoint publishEndpoint;

		public OrderSubmittedDomainEventHandler(IPublishEndpoint publishEndpoint)
		{
			this.publishEndpoint = publishEndpoint;
		}

		/// <inheritdoc />
		public async Task HandleAsync(OrderSubmittedDomainEvent domainEvent, CancellationToken cancellationToken)
		{
			OrderSubmitted message = new OrderSubmitted(domainEvent.Order.ID, domainEvent.Order.Total);

			// Publish the event message on the message bus.
			await this.publishEndpoint.Publish(message);
		}
	}
}
