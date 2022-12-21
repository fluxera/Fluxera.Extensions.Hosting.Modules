namespace Ordering.Domain.CustomerAggregate
{
	using Fluxera.ValueObject;
	using JetBrains.Annotations;

	[PublicAPI]
	public sealed class Name : ValueObject<Name>
	{
		public Name()
		{
		}

		public Name(string firstName, string lastName, string middleName = null)
		{
			this.FirstName = firstName;
			this.LastName = lastName;
			this.MiddleName = middleName;
		}

		public string FirstName { get; set; }

		public string MiddleName { get; set; }

		public string LastName { get; set; }
	}
}
