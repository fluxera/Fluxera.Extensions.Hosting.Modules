namespace Fluxera.Extensions.Hosting.Modules.AspNetCore
{
	using System.Collections.Generic;
	using System.Linq;
	using Fluxera.Extensions.DependencyInjection;
	using JetBrains.Annotations;
	using Microsoft.AspNetCore.Builder;
	using Microsoft.AspNetCore.Http;
	using Microsoft.AspNetCore.HttpOverrides;
	using Microsoft.AspNetCore.Routing;

	/// <summary>
	///     Extension methods for the <see cref="IApplicationInitializationContext" /> type.
	/// </summary>
	[PublicAPI]
	public static class ApplicationInitializationContextExtensions
	{
		// https://learn.microsoft.com/en-us/aspnet/core/host-and-deploy/proxy-load-balancer?view=aspnetcore-7.0

		/// <summary>
		///		Applies forwarded headers to their matching fields on the current request.
		/// <para>
		///		By convention, HTTP proxies forward information from the client in well-known HTTP headers.
		///		The <see cref="ForwardedHeadersMiddleware"/> reads these headers and fills in the associated
		///		fields on HttpContext.
		/// </para>
		/// </summary>
		/// <remarks>>
		///		Forwarded Headers Middleware can run after diagnostics and error handling, but it must be run
		///		before calling UseHsts.
		/// </remarks>
		public static IApplicationInitializationContext UseForwardedHeaders(this IApplicationInitializationContext context)
		{
			IApplicationBuilder app = context.GetApplicationBuilder();
			context.Log("UseForwardedHeaders", _ => app.UseForwardedHeaders());

			return context;
		}

		/// <summary>
		///		Adds a middleware that can log HTTP requests and responses.
		/// </summary>
		/// <param name="context"></param>
		/// <returns></returns>
		public static IApplicationInitializationContext UseHttpLogging(this IApplicationInitializationContext context)
		{
			IApplicationBuilder app = context.GetApplicationBuilder();
			context.Log("UseHttpLogging", _ => app.UseHttpLogging());

			return context;
		}

		/// <summary>
		///		Enables rate limiting for the application.
		/// </summary>
		public static IApplicationInitializationContext UseRateLimiter(this IApplicationInitializationContext context)
		{
			IApplicationBuilder app = context.GetApplicationBuilder();
			context.Log("UseRateLimiter", _ => app.UseRateLimiter());

			return context;
		}

		/// <summary>
		///     Adds middleware for using HSTS, which adds the Strict-Transport-Security header.
		/// </summary>
		public static IApplicationInitializationContext UseHsts(this IApplicationInitializationContext context)
		{
			IApplicationBuilder app = context.GetApplicationBuilder();
			context.Log("UseHsts", _ => app.UseHsts());

			return context;
		}

		/// <summary>
		///     Adds middleware for redirecting HTTP Requests to HTTPS.
		/// </summary>
		public static IApplicationInitializationContext UseHttpsRedirection(this IApplicationInitializationContext context)
		{
			IApplicationBuilder app = context.GetApplicationBuilder();
			context.Log("UseHttpsRedirection", _ => app.UseHttpsRedirection());

			return context;
		}

		/// <summary>
		///     Adds a <see cref="EndpointRoutingMiddleware" /> middleware to the specified <see cref="IApplicationBuilder" />.
		/// </summary>
		/// <param name="context"></param>
		/// <returns></returns>
		public static IApplicationInitializationContext UseRouting(this IApplicationInitializationContext context)
		{
			IApplicationBuilder app = context.GetApplicationBuilder();
			context.Log("UseRouting", _ => app.UseRouting());

			return context;
		}

		/// <summary>
		///     Adds endpoints for controller actions to the <see cref="IEndpointRouteBuilder" /> without specifying any routes.
		///     <br /><br />
		///     Adds a <see cref="EndpointMiddleware" /> middleware to the specified <see cref="IApplicationBuilder" />
		///     with the <see cref="EndpointDataSource" /> instances built from configured <see cref="IEndpointRouteBuilder" />.
		///     The <see cref="EndpointMiddleware" /> will execute the <see cref="Endpoint" /> associated with the current
		///     request.
		/// </summary>
		public static IApplicationInitializationContext UseEndpoints(this IApplicationInitializationContext context)
		{
			IApplicationBuilder app = context.GetApplicationBuilder();
			context.Log("UseEndpoints", _ =>
			{
				app.UseEndpoints(builder =>
				{
					RouteEndpointContributorList contributorList = context.ServiceProvider.GetObjectOrDefault<RouteEndpointContributorList>();
					if(contributorList != null)
					{
						IList<IEndpointRouteContributor> contributors = contributorList
							.OrderBy(contributor => contributor.Position)
							.ToList();

						foreach(IEndpointRouteContributor contributor in contributors)
						{
							contributor.MapRoute(builder, context);
						}
					}
				});
			});

			return context;
		}
	}
}
