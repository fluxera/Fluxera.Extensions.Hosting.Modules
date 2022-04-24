namespace Fluxera.Extensions.Hosting.Modules.AspNetCore.Cors.Contributors
{
	using Fluxera.Extensions.Hosting.Modules.Configuration;

	internal sealed class ConfigureOptionsContributor : ConfigureOptionsContributorBase<CorsOptions>
	{
		/// <inheritdoc />
		public override string SectionName => "AspNetCore:Cors";
	}
}
