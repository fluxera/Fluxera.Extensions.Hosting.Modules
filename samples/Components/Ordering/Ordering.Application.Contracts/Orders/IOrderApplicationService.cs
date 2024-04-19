namespace Ordering.Application.Contracts.Orders
{
	using System.Threading.Tasks;
	using Fluxera.Extensions.Hosting.Modules.Application.Contracts;
	using JetBrains.Annotations;

	[PublicAPI]
	public interface IOrderApplicationService : IApplicationService
	{
		Task<ResultDto<OrderDto[]>> GetOrdersAsync();
	}
}
