namespace Catalog.HttpApi
{
	using Fluxera.Extensions.Hosting;
	using Fluxera.Extensions.Hosting.Modules;
	using Fluxera.Extensions.Hosting.Modules.AspNetCore.HttpApi;
	using Fluxera.Extensions.Hosting.Modules.Configuration;
	using JetBrains.Annotations;

	[PublicAPI]
	[DependsOn<HttpApiModule>]
	[DependsOn<ConfigurationModule>]
	public sealed class CatalogHttpApiModule : ConfigureApplicationModule
	{
		/// <inheritdoc />
		public override void PreConfigure(IApplicationInitializationContext context)
		{
			context.UseProblemDetails();
		}
	}
}
