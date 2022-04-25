namespace WebSample.Model
{
	using Fluxera.Entity;

	public class Person : AggregateRoot<Person, string>
	{
		public string Firstname { get; set; }

		public string Lastname { get; set; }
	}
}
