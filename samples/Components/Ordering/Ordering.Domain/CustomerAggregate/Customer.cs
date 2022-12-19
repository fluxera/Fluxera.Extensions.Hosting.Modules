namespace Ordering.Domain.CustomerAggregate
{
	using Fluxera.Entity;
	using JetBrains.Annotations;
	using Ordering.Domain.Shared.CustomerAggregate;

	[PublicAPI]
	public sealed class Customer : AggregateRoot<Customer, CustomerId>
	{
		public Name Name { get; set; }
	}
}
