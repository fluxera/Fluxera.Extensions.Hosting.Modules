namespace Fluxera.Extensions.Hosting.Modules.AutoMapper.UnitTests
{
	using global::AutoMapper;

	public class TestProfile : Profile
	{
		public TestProfile()
		{
			this.CreateMap<Test1, Test2>().ReverseMap();
		}
	}
}
