namespace WebSample.Model
{
	using Fluxera.Entity;

	public class Customer : AggregateRoot<Customer, string>
	{
		public string Name { get; set; }

		public int Number { get; set; }
	}
}
