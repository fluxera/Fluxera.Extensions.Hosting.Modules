namespace Fluxera.Extensions.Hosting.Modules.AutoMapper.UnitTests
{
	public class TestMappingProfileContributor : IMappingProfileContributor
	{
		/// <inheritdoc />
		public void ConfigureProfiles(AutoMapperOptions options)
		{
			options.AddProfile<TestProfile>();
		}
	}
}
