namespace Ordering.Domain.Orders.DomainEvents
{
	using Fluxera.Entity.DomainEvents;
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
