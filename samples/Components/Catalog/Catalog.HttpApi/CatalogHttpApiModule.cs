namespace Catalog.HttpApi
{
	using Fluxera.Extensions.Hosting;
	using Fluxera.Extensions.Hosting.Modules;
	using Fluxera.Extensions.Hosting.Modules.AspNetCore.HttpApi;
	using JetBrains.Annotations;

	[PublicAPI]
	[DependsOn<HttpApiModule>]
	public sealed class CatalogHttpApiModule : ConfigureApplicationModule
	{
		/// <inheritdoc />
		public override void PreConfigure(IApplicationInitializationContext context)
		{
			context.UseProblemDetails();
		}
	}
}
