namespace Ordering.Application.Orders.Handlers
{
	using System.Threading;
	using System.Threading.Tasks;
	using Fluxera.Entity.DomainEvents;
	using JetBrains.Annotations;
	using MassTransit;
	using Ordering.Domain.Orders.DomainEvents;

	[UsedImplicitly]
	public sealed class OrderSubmittedHandler : IDomainEventHandler<OrderSubmitted>
	{
		private readonly IPublishEndpoint publishEndpoint;

		public OrderSubmittedHandler(IPublishEndpoint publishEndpoint)
		{
			this.publishEndpoint = publishEndpoint;
		}

		/// <inheritdoc />
		public async Task HandleAsync(OrderSubmitted domainEvent, CancellationToken cancellationToken)
		{
			// Publish the event message on the message bus.
			await this.publishEndpoint.Publish(new Contracts.Orders.Messages.OrderSubmitted(domainEvent.Order.ID, domainEvent.Order.Total), cancellationToken);
		}
	}
}
