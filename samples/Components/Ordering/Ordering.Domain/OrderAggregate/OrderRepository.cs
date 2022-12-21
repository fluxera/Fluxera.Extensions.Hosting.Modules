namespace Ordering.Domain.OrderAggregate
{
	using Fluxera.Repository;
	using JetBrains.Annotations;
	using Ordering.Domain.Shared.OrderAggregate;

	/// <summary>
	///     An implementation of a generic repository that handles order instances.
	/// </summary>
	[UsedImplicitly]
	internal sealed class OrderRepository : Repository<Order, OrderId>, IOrderRepository
	{
		/// <inheritdoc />
		public OrderRepository(IRepository<Order, OrderId> innerRepository)
			: base(innerRepository)
		{
		}
	}
}
