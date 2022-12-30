namespace Ordering.Application.Contracts.Orders
{
	using Fluxera.Extensions.Hosting.Modules.Application.Contracts.Dtos;
	using JetBrains.Annotations;
	using Ordering.Domain.Shared.OrderAggregate;

	/// <summary>
	///     A dto that provides the data of an order.
	/// </summary>
	[PublicAPI]
	public sealed class OrderDto : EntityDto<OrderId>
	{
		public string OrderNumber { get; set; }
	}
}
