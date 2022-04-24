namespace WebSample.Model
{
	using System.ComponentModel.DataAnnotations;
	using Fluxera.Entity;

	public class Customer : AggregateRoot<Customer, string>
	{
		[Required]
		[StringLength(100)]
		public string Name { get; set; }

		[Required]
		public int Number { get; set; }
	}
}
