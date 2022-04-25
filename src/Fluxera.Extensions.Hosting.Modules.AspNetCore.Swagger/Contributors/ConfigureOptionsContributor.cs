namespace Fluxera.Extensions.Hosting.Modules.AspNetCore.Swagger.Contributors
{
	using Fluxera.Extensions.Hosting.Modules.Configuration;

	internal sealed class ConfigureOptionsContributor : ConfigureOptionsContributorBase<SwaggerOptions>
	{
		/// <inheritdoc />
		public override string SectionName => "AspNetCore:HttpApi:Swagger";
	}
}
