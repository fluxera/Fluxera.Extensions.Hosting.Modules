namespace Example.HttpApi
{
	using Fluxera.Extensions.Hosting;
	using Fluxera.Extensions.Hosting.Modules;
	using Fluxera.Extensions.Hosting.Modules.AspNetCore.HttpApi;
	using JetBrains.Annotations;

	[PublicAPI]
	[DependsOn(typeof(HttpApiModule))]
	public sealed class ExampleHttpApiModule : ConfigureApplicationModule
	{
		/// <inheritdoc />
		public override void PreConfigure(IApplicationInitializationContext context)
		{
			context.UseProblemDetails();
		}
	}
}
