namespace Ordering.Infrastructure.Customers
{
	using Fluxera.Repository;
	using JetBrains.Annotations;
	using Ordering.Domain.Customers;
	using Ordering.Domain.Shared.Customers;

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
