namespace Ordering.Domain.Orders.DomainEvents
{
	using Fluxera.Entity.DomainEvents;
	using JetBrains.Annotations;

	[PublicAPI]
	public sealed class OrderSubmittedDomainEvent : IDomainEvent
	{
		public OrderSubmittedDomainEvent(Order order)
		{
			this.Order = order;
		}

		public Order Order { get; }
	}
}
