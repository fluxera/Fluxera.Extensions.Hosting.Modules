namespace Ordering.Domain.CustomerAggregate
{
	using Fluxera.Repository;
	using JetBrains.Annotations;
	using Ordering.Domain.Shared.CustomerAggregate;

	/// <summary>
	///     An implementation of a generic repository that handles customer instances.
	/// </summary>
	[UsedImplicitly]
	internal sealed class CustomerRepository : Repository<Customer, CustomerId>, ICustomerRepository
	{
		/// <inheritdoc />
		public CustomerRepository(IRepository<Customer, CustomerId> innerRepository)
			: base(innerRepository)
		{
		}
	}
}
