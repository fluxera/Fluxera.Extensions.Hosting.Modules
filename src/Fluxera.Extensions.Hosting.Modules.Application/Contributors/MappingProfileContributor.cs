namespace Fluxera.Extensions.Hosting.Modules.Application.Contributors
{
	using Fluxera.Extensions.Hosting;
	using Fluxera.Extensions.Hosting.Modules.Application.Mappings;
	using Fluxera.Extensions.Hosting.Modules.AutoMapper;
	using JetBrains.Annotations;

	[UsedImplicitly]
	internal sealed class MappingProfileContributor : MappingProfileContributorBase
    {
		/// <inheritdoc />
		public override void ConfigureProfiles(AutoMapperOptions options, IApplicationInitializationContext context)
		{
			options.AddProfile<ResultMappingProfile>();
		}
	}
}
