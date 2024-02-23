namespace Fluxera.Extensions.Hosting.Modules.AspNetCore.Blazor
{
	using System;
	using System.Reflection;
	using JetBrains.Annotations;
	using Microsoft.AspNetCore.Builder;
	using Microsoft.AspNetCore.Routing;

	/// <summary>
	///		Extension methods for the <see cref="IEndpointRouteBuilder"/> type.
	/// </summary>
	[PublicAPI]
	public static class EndpointRouteBuilderExtensions
	{
		///  <summary>
		/// 		Maps the page components defined in the given <paramref name="rootComponentType"/> to the given assembly
		/// 		and renders the component specified by <paramref name="rootComponentType"/> when the route matches.
		///  </summary>
		///  <param name="endpoints">The <see cref="IEndpointRouteBuilder"/>.</param>
		///  <param name="rootComponentType"></param>
		///  <returns>A <see cref="RazorComponentsEndpointConventionBuilder"/> that can be used to further configure the API.</returns>
		public static RazorComponentsEndpointConventionBuilder MapRazorComponents(this IEndpointRouteBuilder endpoints, Type rootComponentType)
		{
			MethodInfo method = typeof(RazorComponentsEndpointRouteBuilderExtensions).GetMethod("MapRazorComponents");
			MethodInfo genericMethod = method?.MakeGenericMethod(rootComponentType);

			if(genericMethod?.Invoke(null, [endpoints]) is not RazorComponentsEndpointConventionBuilder builder)
			{
				throw new InvalidOperationException("No blazor contributor was found.");
			}

			return builder;
		}
	}
}
