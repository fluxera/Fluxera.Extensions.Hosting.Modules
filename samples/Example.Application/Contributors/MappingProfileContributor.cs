namespace Example.Application.Contributors
{
	using Fluxera.Extensions.Hosting;
	using Fluxera.Extensions.Hosting.Modules.AutoMapper;
	using JetBrains.Annotations;
	using Microsoft.Extensions.DependencyInjection;

	[UsedImplicitly]
	internal sealed class MappingProfileContributor : IMappingProfileContributor
	{
		/// <inheritdoc />
		public void ConfigureProfileServices(IServiceConfigurationContext context)
		{
			context.Log("AddMappingProfiles",
				services => services.AddTransient<MappingProfile>());
		}

		/// <inheritdoc />
		public void ConfigureProfiles(AutoMapperOptions options, IApplicationInitializationContext context)
		{
			context.Log("AddMappingProfiles",
				_ => options.AddProfile<MappingProfile>());
		}
	}
}
