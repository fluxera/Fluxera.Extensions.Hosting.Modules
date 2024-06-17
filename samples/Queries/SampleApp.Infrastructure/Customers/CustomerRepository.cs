namespace SampleApp.Infrastructure.Customers
{
	using Fluxera.Repository;
	using JetBrains.Annotations;
	using SampleApp.Domain.Customers;
	using SampleApp.Domain.Shared.Customers;

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
