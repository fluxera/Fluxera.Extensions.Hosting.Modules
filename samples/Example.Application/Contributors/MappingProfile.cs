namespace Example.Application.Contributors
{
	using AutoMapper;
	using Example.Application.Contracts.Dtos;
	using Example.Domain.Example;
	using JetBrains.Annotations;

	[UsedImplicitly]
	internal sealed class MappingProfile : Profile
	{
		public MappingProfile()
		{
			this.CreateMap<Example, ExampleDto>().ReverseMap();
		}
	}
}
