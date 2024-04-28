namespace Ordering.Domain.Orders.DomainEvents
{
	using Fluxera.DomainEvents.Abstractions;
	using JetBrains.Annotations;

	[PublicAPI]
	public sealed class OrderShipped : IDomainEvent
	{
		public OrderShipped(Order order)
		{
			this.Order = order;
		}

		public Order Order { get; }
	}
}
