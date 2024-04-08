namespace Ordering.Domain.Customers
{
	using Fluxera.Entity;
	using JetBrains.Annotations;
	using Ordering.Domain.Shared.Customers;

	[PublicAPI]
	public sealed class Customer : AggregateRoot<Customer, CustomerId>
	{
		public Name Name { get; set; }
	}
}
