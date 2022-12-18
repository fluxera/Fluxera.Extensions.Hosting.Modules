namespace Catalog.Application.Contributors
{
	using AutoMapper;
	using Catalog.Application.Contracts.Dtos;
	using Catalog.Domain.Product;
	using JetBrains.Annotations;

	[UsedImplicitly]
	internal sealed class MappingProfile : Profile
	{
		public MappingProfile()
		{
			this.CreateMap<Product, ProductDto>().ReverseMap();
		}
	}
}
