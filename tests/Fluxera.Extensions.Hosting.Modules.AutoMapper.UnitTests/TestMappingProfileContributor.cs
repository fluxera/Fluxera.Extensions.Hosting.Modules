namespace Fluxera.Extensions.Hosting.Modules.AutoMapper.UnitTests
{
	using Microsoft.Extensions.DependencyInjection;

	public class TestMappingProfileContributor : IMappingProfileContributor
	{
		/// <inheritdoc />
		public void ConfigureProfileServices(IServiceConfigurationContext context)
		{
			context.Services.AddTransient<TestProfile>();
		}

		/// <inheritdoc />
		public void ConfigureProfiles(AutoMapperOptions options, IApplicationInitializationContext context)
		{
			options.AddProfile<TestProfile>();
		}
	}
}
