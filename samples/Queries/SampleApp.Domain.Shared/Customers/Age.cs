namespace SampleApp.Domain.Shared.Customers
{
	using Fluxera.ValueObject;
	using JetBrains.Annotations;

	[PublicAPI]
	public sealed class Age : PrimitiveValueObject<Age, int>
	{
		/// <inheritdoc />
		public Age(int value) : base(value)
		{
		}

		/**
		 * The comparison operators are needed int the type metadata
		 * to create query expressions.
		 */

		public static bool operator <(Age left, Age right)
		{
			return left.CompareTo(right) < 0;
		}

		public static bool operator <=(Age left, Age right)
		{
			return left.CompareTo(right) <= 0;
		}

		public static bool operator >(Age left, Age right)
		{
			return left.CompareTo(right) > 0;
		}

		public static bool operator >=(Age left, Age right)
		{
			return left.CompareTo(right) >= 0;
		}
	}
}
