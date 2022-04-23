namespace Fluxera.Extensions.Hosting.Modules.AspNetCore
{
	using System.Collections.Generic;
	using System.Linq;
	using Fluxera.Extensions.DependencyInjection;
	using JetBrains.Annotations;
	using Microsoft.AspNetCore.Builder;
	using Microsoft.AspNetCore.Routing;

	/// <summary>
	///     Extension methods for the <see cref="IApplicationInitializationContext" /> type.
	/// </summary>
	[PublicAPI]
	public static class ApplicationInitializationContextExtensions
	{
		/// <summary>
		///     Adds middleware for redirecting HTTP Requests to HTTPS.
		/// </summary>
		public static IApplicationInitializationContext UseHttpsRedirection(this IApplicationInitializationContext context)
		{
			WebApplication app = context.GetApplicationBuilder();
			context.Log("UseHttpsRedirection", _ => app.UseHttpsRedirection());

			return context;
		}

		/// <summary>
		///     Adds endpoints for controller actions to the <see cref="IEndpointRouteBuilder" /> without specifying any routes.
		/// </summary>
		public static IApplicationInitializationContext UseEndpoints(this IApplicationInitializationContext context)
		{
			WebApplication app = context.GetApplicationBuilder();
			context.Log("UseEndpoints", _ =>
			{
				app.UseRouting();
				app.UseEndpoints(builder =>
				{
					IList<IRouteEndpointContributor> contributors = context.ServiceProvider
						.GetObject<RouteEndpointContributorList>()
						.OrderBy(contributor => contributor.Position)
						.ToList();

					foreach(IRouteEndpointContributor contributor in contributors)
					{
						contributor.MapRoute(builder);
					}
				});
			});

			return context;
		}
	}
}
