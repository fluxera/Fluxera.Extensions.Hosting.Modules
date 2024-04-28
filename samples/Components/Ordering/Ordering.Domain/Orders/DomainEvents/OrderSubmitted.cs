namespace Ordering.Domain.Orders.DomainEvents
{
	using Fluxera.DomainEvents.Abstractions;
	using JetBrains.Annotations;

	[PublicAPI]
	public sealed class OrderSubmitted : IDomainEvent
	{
		public OrderSubmitted(Order order)
		{
			this.Order = order;
		}

		public Order Order { get; }
	}
}
