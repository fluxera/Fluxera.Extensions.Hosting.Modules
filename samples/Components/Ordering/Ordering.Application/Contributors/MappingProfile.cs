namespace Ordering.Application.Contributors
{
	using AutoMapper;
	using JetBrains.Annotations;
	using Ordering.Application.Contracts.Orders;
	using Ordering.Domain.OrderAggregate;

	[UsedImplicitly]
	internal sealed class MappingProfile : Profile
	{
		public MappingProfile()
		{
			this.CreateMap<Order, OrderDto>().ReverseMap();
		}
	}
}
