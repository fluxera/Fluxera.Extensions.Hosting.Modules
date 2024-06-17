namespace SampleApp.Domain.Customers
{
	using Fluxera.ValueObject;
	using JetBrains.Annotations;
	using SampleApp.Domain.Shared.Customers;

	[PublicAPI]
	public sealed class Address : ValueObject<Address>
	{
		public string Street { get; set; }

		public string Number { get; set; }

		public string City { get; set; }

		public ZipCode ZipCode { get; set; }

		public Country Country { get; set; }

		public decimal IgnoreMe { get; set; }
	}
}
