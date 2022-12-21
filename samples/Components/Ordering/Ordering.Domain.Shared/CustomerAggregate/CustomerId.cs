namespace Ordering.Domain.Shared.CustomerAggregate
{
	using Fluxera.StronglyTypedId;
	using JetBrains.Annotations;

	[PublicAPI]
	public sealed class CustomerId : StronglyTypedId<CustomerId, string>
	{
		/// <inheritdoc />
		public CustomerId(string value) : base(value)
		{
		}
	}
}
