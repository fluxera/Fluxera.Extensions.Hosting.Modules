namespace SampleApp.Domain.Customers
{
	using Fluxera.Repository;
	using JetBrains.Annotations;
	using SampleApp.Domain.Shared.Customers;

	[PublicAPI]
	public interface ICustomerRepository : IRepository<Customer, CustomerId>
	{
	}
}
