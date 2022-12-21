namespace Ordering.Domain.Shared.OrderAggregate
{
	using Fluxera.StronglyTypedId;
	using JetBrains.Annotations;

	[PublicAPI]
	public sealed class OrderItemId : StronglyTypedId<OrderItemId, string>
	{
		/// <inheritdoc />
		public OrderItemId(string value) : base(value)
		{
		}
	}
}
