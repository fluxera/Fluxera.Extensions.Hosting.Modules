namespace Ordering.Domain.CustomerAggregate
{
	using Fluxera.Repository;
	using JetBrains.Annotations;
	using Ordering.Domain.Shared.CustomerAggregate;

	/// <summary>
	///     A contract for a repository that handles customer instances.
	/// </summary>
	[PublicAPI]
	public interface ICustomerRepository : IRepository<Customer, CustomerId>
	{
	}
}
