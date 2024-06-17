namespace SampleApp.Application.Contracts.Customers
{
	using JetBrains.Annotations;
	using SampleApp.Domain.Shared.Customers;

	[PublicAPI]
	public sealed class CustomerDto
	{
		public CustomerId ID { get; set; }

		public string FirstName { get; set; }

		public string LastName { get; set; }

		public string Email { get; set; }

		public Age Age { get; set; }

		public CustomerState State { get; set; }

		public AddressDto Address { get; set; }

		public double IgnoreMe { get; set; }
	}
}
