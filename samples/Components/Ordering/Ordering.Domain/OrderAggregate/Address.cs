namespace Ordering.Domain.OrderAggregate
{
	using Fluxera.ValueObject;
	using JetBrains.Annotations;

	[PublicAPI]
	public sealed class Address : ValueObject<Address>
	{
		public Address()
		{
		}

		public Address(string street, string city, string state, string country, string zipcode)
		{
			this.Street = street;
			this.City = city;
			this.State = state;
			this.Country = country;
			this.ZipCode = zipcode;
		}

		public string Street { get; set; }

		public string City { get; set; }

		public string State { get; set; }

		public string Country { get; set; }

		public string ZipCode { get; set; }
	}
}
