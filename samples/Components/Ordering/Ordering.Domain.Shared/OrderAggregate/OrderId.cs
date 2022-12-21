namespace Ordering.Domain.Shared.OrderAggregate
{
	using Fluxera.StronglyTypedId;
	using JetBrains.Annotations;

	[PublicAPI]
	public sealed class OrderId : StronglyTypedId<OrderId, string>
	{
		/// <inheritdoc />
		public OrderId(string value) : base(value)
		{
		}
	}
}
