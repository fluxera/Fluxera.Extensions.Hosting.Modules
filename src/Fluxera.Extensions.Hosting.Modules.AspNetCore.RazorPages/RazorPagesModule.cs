namespace Fluxera.Extensions.Hosting.Modules.AspNetCore.RazorPages
{
	using Fluxera.Extensions.Hosting.Modules.AspNetCore.RazorPages.Contributors;
	using JetBrains.Annotations;
	using Microsoft.Extensions.DependencyInjection;

	/// <summary>
	///     A module that enables Razor Pages for ASP.NET Core.
	/// </summary>
	[PublicAPI]
	[DependsOn<AspNetCoreModule>]
	public sealed class RazorPagesModule : ConfigureServicesModule
	{
		/// <inheritdoc />
		public override void PreConfigureServices(IServiceConfigurationContext context)
		{
			// Add the route endpoint contributor.
			context.Services.AddEndpointRouteContributor<EndpointRouteContributor>();
		}

		/// <inheritdoc />
		public override void ConfigureServices(IServiceConfigurationContext context)
		{
			context.Log("AddRazorPages", services => services.AddRazorPages());
		}
	}
}
