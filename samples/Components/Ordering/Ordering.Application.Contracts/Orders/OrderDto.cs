namespace Ordering.Application.Contracts.Orders
{
	using Fluxera.Extensions.Hosting.Modules.Application.Contracts.Dtos;
	using JetBrains.Annotations;

	/// <summary>
	///     A dto that provides the data of an order.
	/// </summary>
	[PublicAPI]
	public sealed class OrderDto : EntityDto<string>
	{
		public string OrderNumber { get; set; }

		///// <inheritdoc />
		//protected override OrderId CreateKey(string entityId)
		//{
		//	return new OrderId(entityId);
		//}
	}
}
