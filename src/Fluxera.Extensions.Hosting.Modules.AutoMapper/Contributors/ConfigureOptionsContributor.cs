namespace Fluxera.Extensions.Hosting.Modules.AutoMapper.Contributors
{
	using Fluxera.Extensions.Hosting.Modules.Configuration;

	internal sealed class ConfigureOptionsContributor : ConfigureOptionsContributorBase<AutoMapperOptions>
	{
		/// <inheritdoc />
		public override string SectionName => "AutoMapper";
	}
}
