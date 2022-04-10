namespace Fluxera.Extensions.Hosting.Modules.AutoMapper.UnitTests
{
	[DependsOn(typeof(AutoMapperModule))]
	public class TestModule : ConfigureServicesModule
	{
		/// <inheritdoc />
		public override void ConfigureServices(IServiceConfigurationContext context)
		{
			context.Services.AddMappingProfileContributor<TestMappingProfileContributor>();
		}
	}
}
