namespace Ordering.Application.Orders
{
	using JetBrains.Annotations;
	using MediatR;
	using Ordering.Application.Contracts.Orders;

	[UsedImplicitly]
	internal sealed class OrderApplicationService : IOrderApplicationService
	{
		private readonly ISender sender;

		public OrderApplicationService(ISender sender)
		{
			this.sender = sender;
		}
	}
}
