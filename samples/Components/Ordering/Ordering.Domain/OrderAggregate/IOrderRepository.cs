namespace Ordering.Domain.OrderAggregate
{
	using Fluxera.Repository;
	using JetBrains.Annotations;
	using Ordering.Domain.Shared.OrderAggregate;

	/// <summary>
	///     A contract for a repository that handles order instances.
	/// </summary>
	[PublicAPI]
	public interface IOrderRepository : IRepository<Order, OrderId>
	{
	}
}
