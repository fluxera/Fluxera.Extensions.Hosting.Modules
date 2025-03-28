namespace Fluxera.Extensions.Hosting.Modules.MultiTenancy.UnitTests
{
	using Fluxera.Entity;

	public class Customer : Entity<Customer, string>
	{
		public string Name { get; set; }
	}
}
