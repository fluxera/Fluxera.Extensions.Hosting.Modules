namespace SampleApp.Application.Contracts.Customers
{
	using JetBrains.Annotations;
	using SampleApp.Domain.Shared.Customers;

	[PublicAPI]
	public sealed class AddressDto
	{
		public string Street { get; set; }

		public string Number { get; set; }

		public string City { get; set; }

		public ZipCode ZipCode { get; set; }

		public CountryDto Country { get; set; }

		public decimal IgnoreMe { get; set; }
	}
}
