namespace Fluxera.Extensions.Hosting.Modules.Persistence.UnitTests
{
	using Fluxera.Entity;

	public class Customer : AggregateRoot<Customer, string>
	{
		public string Name { get; set; }
	}
}
