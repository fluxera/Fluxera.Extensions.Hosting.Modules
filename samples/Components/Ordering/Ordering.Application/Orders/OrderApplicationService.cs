// ReSharper disable NotAccessedField.Local

namespace Ordering.Application.Orders
{
	using System;
	using System.Threading.Tasks;
	using AutoMapper;
	using Fluxera.Extensions.Hosting.Modules.Application.Contracts;
	using JetBrains.Annotations;
	using MadEyeMatt.Results;
	using MediatR;
	using Ordering.Application.Contracts.Orders;

	[UsedImplicitly]
	internal sealed class OrderApplicationService : IOrderApplicationService
	{
		private readonly IMediator mediator;
		private readonly IMapper mapper;

		public OrderApplicationService(IMediator mediator, IMapper mapper)
		{
			this.mediator = mediator;
			this.mapper = mapper;
		}

		/// <inheritdoc />
		public Task<ResultDto<OrderDto[]>> GetOrdersAsync()
		{
			Result<OrderDto[]> result = Result.Ok(Array.Empty<OrderDto>());
			ResultDto<OrderDto[]> resultDto = this.mapper.Map<ResultDto<OrderDto[]>>(result);

			return Task.FromResult(resultDto);
		}
	}
}
