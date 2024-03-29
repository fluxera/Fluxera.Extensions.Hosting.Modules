﻿namespace Catalog.Application.Contributors
{
	using AutoMapper;
	using Catalog.Application.Contracts.Products;
	using Catalog.Domain.ProductAggregate;
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
