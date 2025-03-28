namespace Fluxera.Extensions.Hosting.Modules.AspNetCore.Blazor
{
	using Fluxera.Extensions.DependencyInjection;
	using JetBrains.Annotations;
	using Microsoft.AspNetCore.Builder;
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Reflection;
	using Microsoft.AspNetCore.Components.WebAssembly.Server;

	/// <summary>
	///     Extension methods for the <see cref="IApplicationInitializationContext" /> type.
	/// </summary>
	[PublicAPI]
	public static class ApplicationInitializationContextExtensions
	{
		/// <summary>
		///		Adds middleware needed for debugging Blazor WebAssembly applications
		///		inside Chromium dev tools.
		/// </summary>
		public static void UseWebAssemblyDebugging(this IApplicationInitializationContext context)
		{
			IApplicationBuilder app = context.GetApplicationBuilder();
			context.Log("UseWebAssemblyDebugging", _ => app.UseWebAssemblyDebugging());
		}

		/// <summary>
		///		Maps the page components defined in the specified <typeparamref name="TRootComponent"/> to the given assembly
		///		and renders the component specified by <typeparamref name="TRootComponent"/> when the route matches.
		/// </summary>
		public static IApplicationInitializationContext UseRazorComponents<TRootComponent>(this IApplicationInitializationContext context, Action<WebAssemblyComponentsEndpointOptions> configure = null)
		{
			WebApplication app = (WebApplication)context.GetApplicationBuilder();
			context.Log("UseRazorComponents", serviceProvider =>
			{
				BlazorAssembliesContributorList contributorList = serviceProvider.GetObjectOrDefault<BlazorAssembliesContributorList>();
				IEnumerable<Assembly> assemblies = contributorList.SelectMany(x => x.AdditionalAssemblies).Distinct();

				return app.MapRazorComponents<TRootComponent>()
					.AddInteractiveWebAssemblyRenderMode(configure)
					.AddAdditionalAssemblies(assemblies.ToArray());
			});

			return context;
		}
	}
}
