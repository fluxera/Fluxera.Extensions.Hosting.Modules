namespace Fluxera.Extensions.Hosting.Modules.AspNetCore
{
	using Fluxera.Guards;
	using JetBrains.Annotations;
	using Microsoft.AspNetCore.Mvc;
	using Microsoft.AspNetCore.Mvc.Routing;

	/// <summary>
	///     Extension methods for the <see cref="MvcOptions" /> type.
	/// </summary>
	[PublicAPI]
	public static class MvcOptionsExtensions
	{
		/// <summary>
		///     Sets the given prefix for every route.
		/// </summary>
		/// <param name="options"></param>
		/// <param name="prefix"></param>
		public static void UseGeneralRoutePrefix(this MvcOptions options, string prefix)
		{
			Guard.Against.NullOrWhiteSpace(prefix, nameof(prefix));

			options.UseGeneralRoutePrefix(new RouteAttribute(prefix));
		}

		private static void UseGeneralRoutePrefix(this MvcOptions options, IRouteTemplateProvider routeAttribute)
		{
			options.Conventions.Add(new RoutePrefixConvention(routeAttribute));
		}
	}
}
