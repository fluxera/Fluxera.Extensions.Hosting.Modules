namespace Ordering.Domain.Orders
{
	using Fluxera.Repository;
	using JetBrains.Annotations;
	using Ordering.Domain.Shared.Orders;

	/// <summary>
	///     A contract for a repository that handles order instances.
	/// </summary>
	[PublicAPI]
	public interface IOrderRepository : IRepository<Order, OrderId>
	{
	}
}
