namespace Ordering.Application.Services
{
	using JetBrains.Annotations;
	using MediatR;
	using Ordering.Application.Contracts.Services;

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
