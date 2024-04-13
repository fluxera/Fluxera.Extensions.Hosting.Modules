namespace Ordering.Domain.Orders
{
	using Fluxera.Enumeration;

	public sealed class OrderState : Enumeration<OrderState>
	{
		public static OrderState Submitted = new OrderState(1, nameof(Submitted));
		public static OrderState Shipped = new OrderState(2, nameof(Shipped));

		/// <inheritdoc />
		private OrderState(int value, string name)
			: base(value, name)
		{
		}
	}
}
