namespace SampleApp.Application.Contributors
{
	using Fluxera.Extensions.Hosting;
	using Fluxera.Extensions.Hosting.Modules.AutoMapper;
	using JetBrains.Annotations;
	using Microsoft.Extensions.DependencyInjection;
	using SampleApp.Application.Mappings;

	[UsedImplicitly]
	internal sealed class MappingProfileContributor : IMappingProfileContributor
	{
		/// <inheritdoc />
		public void ConfigureProfileServices(IServiceConfigurationContext context)
		{
			context.Log("AddMappingProfiles", services =>
			{
				services.AddTransient<CustomerProfile>();
			});
		}

		/// <inheritdoc />
		public void ConfigureProfiles(AutoMapperOptions options, IApplicationInitializationContext context)
		{
			context.Log("AddMappingProfiles", _ => options.AddProfile<CustomerProfile>());
		}
	}
}
