namespace Ordering.Infrastructure.Orders
{
	using Fluxera.Repository;
	using JetBrains.Annotations;
	using Ordering.Domain.Orders;
	using Ordering.Domain.Shared.Orders;

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
