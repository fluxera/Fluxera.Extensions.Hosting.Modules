namespace Fluxera.Extensions.Hosting.Modules.AspNetCore.Blazor
{
	using Fluxera.Extensions.DependencyInjection;
	using JetBrains.Annotations;
	using MadEyeMatt.AspNetCore.Blazor;
	using Microsoft.Extensions.DependencyInjection;

	/// <summary>
	///     A module that enables Razor Pages for ASP.NET Core.
	/// </summary>
	[PublicAPI]
	[DependsOn<AspNetCoreModule>]
	public sealed class BlazorModule : ConfigureServicesModule
	{
		/// <inheritdoc />
		public override void PreConfigureServices(IServiceConfigurationContext context)
		{
			// Add the contributor list.
			context.Log("AddObjectAccessor(BlazorContributorList)",
				services => services.AddObjectAccessor(new BlazorAssembliesContributorList(), ObjectAccessorLifetime.Application));

			// Add the assembly provider.
			context.Log("AddBlazorAssembliesProvider",
				services => services.AddTransient<IBlazorAssembliesProvider, BlazorAssembliesProvider>());
		}

		/// <inheritdoc />
		public override void ConfigureServices(IServiceConfigurationContext context)
		{
			context.Log("AddComponentActivator",
				services => services.AddComponentActivator());

			context.Log("AddRazorComponents", services => services
				.AddRazorComponents()
				.AddInteractiveWebAssemblyComponents());
		}
	}
}
