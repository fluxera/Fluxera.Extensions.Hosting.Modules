namespace IdentityService
{
	using Fluxera.Extensions.Hosting;
	using Fluxera.Extensions.Hosting.Modules;
	using Fluxera.Extensions.Hosting.Modules.AspNetCore;
	using Fluxera.Extensions.Hosting.Modules.AspNetCore.HealthChecks;
	using Fluxera.Extensions.Hosting.Modules.AspNetCore.Warmup;
	using JetBrains.Annotations;

	[PublicAPI]
	[DependsOn(typeof(WarmupModule))]
	[DependsOn(typeof(HealthChecksModule))]
	[DependsOn(typeof(AspNetCoreModule))]
	public class IdentityServiceModule : ConfigureApplicationModule
	{
		/// <inheritdoc />
		public override void Configure(IApplicationInitializationContext context)
		{
			context.UseHttpsRedirection();

			context.UseRouting();

			context.UseEndpoints();
		}
	}
}
