namespace Fluxera.Extensions.Hosting.Modules.AspNetCore
{
	using System;
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

#if NET7_0_OR_GREATER
		/// <summary>
		///		Enables rate limiting for the application.
		/// </summary>
		public static IApplicationInitializationContext UseRateLimiter(this IApplicationInitializationContext context)
		{
			IApplicationBuilder app = context.GetApplicationBuilder();
			context.Log("UseRateLimiter", _ => app.UseRateLimiter());

			return context;
		}
#endif

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
		///		Enables static file serving for the current request path.
		/// </summary>
		public static IApplicationInitializationContext UseStaticFiles(this IApplicationInitializationContext context)
		{
			IApplicationBuilder app = context.GetApplicationBuilder();
			context.Log("UseStaticFiles", _ => app.UseStaticFiles());

			return context;
		}

		/// <summary>
		///		Enables static file serving for the given request path.
		/// </summary>
		public static IApplicationInitializationContext UseStaticFiles(this IApplicationInitializationContext context, string requestPath)
		{
			IApplicationBuilder app = context.GetApplicationBuilder();
			context.Log("UseStaticFiles", _ => app.UseStaticFiles(requestPath));

			return context;
		}

		/// <summary>
		///		Enables static file serving with the given options.
		/// </summary>
		public static IApplicationInitializationContext UseStaticFiles(this IApplicationInitializationContext context, StaticFileOptions options)
		{
			IApplicationBuilder app = context.GetApplicationBuilder();
			context.Log("UseStaticFiles", _ => app.UseStaticFiles(options));

			return context;
		}

#if NET8_0_OR_GREATER
		/// <summary>
		///		Adds the anti-forgery middleware to the pipeline.
		/// </summary>
		public static IApplicationInitializationContext UseAntiforgery(this IApplicationInitializationContext context)
		{
			IApplicationBuilder app = context.GetApplicationBuilder();
			context.Log("UseAntiforgery", _ => app.UseAntiforgery());

			return context;
		}
#endif

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

		/// <summary>
		///		Adds a middleware to the pipeline that will catch exceptions, log them, and re-execute the request in an alternate pipeline.
		///		The request will not be re-executed if the response has already started.
		/// </summary>
		public static IApplicationInitializationContext UseExceptionHandler(this IApplicationInitializationContext context)
		{
			IApplicationBuilder app = context.GetApplicationBuilder();
			context.Log("UseExceptionHandler", _ => app.UseExceptionHandler());

			return context;
		}

		/// <summary>
		///		Adds a middleware to the pipeline that will catch exceptions, log them, reset the request path, and re-execute the request.
		///		The request will not be re-executed if the response has already started.
		/// </summary>
		public static IApplicationInitializationContext UseExceptionHandler(this IApplicationInitializationContext context, string errorHandlingPath)
		{
			IApplicationBuilder app = context.GetApplicationBuilder();
			context.Log("UseExceptionHandler", _ => app.UseExceptionHandler(errorHandlingPath));

			return context;
		}

#if NET8_0_OR_GREATER
		/// <summary>
		///		Adds a middleware to the pipeline that will catch exceptions, log them, reset the request path, and re-execute the request.
		///		The request will not be re-executed if the response has already started.
		/// </summary>
		public static IApplicationInitializationContext UseExceptionHandler(this IApplicationInitializationContext context, string errorHandlingPath, bool createScopeForErrors)
		{
			IApplicationBuilder app = context.GetApplicationBuilder();
			context.Log("UseExceptionHandler", _ => app.UseExceptionHandler(errorHandlingPath, createScopeForErrors));

			return context;
		}
#endif

		/// <summary>
		///		Adds a middleware to the pipeline that will catch exceptions, log them, and re-execute the request in an alternate pipeline.
		///		The request will not be re-executed if the response has already started.
		/// </summary>
		public static IApplicationInitializationContext UseExceptionHandler(this IApplicationInitializationContext context, Action<IApplicationBuilder> configure)
		{
			IApplicationBuilder app = context.GetApplicationBuilder();
			context.Log("UseExceptionHandler", _ => app.UseExceptionHandler(configure));

			return context;
		}

		/// <summary>
		///		Adds a middleware to the pipeline that will catch exceptions, log them, and re-execute the request in an alternate pipeline.
		///		The request will not be re-executed if the response has already started.
		/// </summary>
		public static IApplicationInitializationContext UseExceptionHandler(this IApplicationInitializationContext context, ExceptionHandlerOptions options)
		{
			IApplicationBuilder app = context.GetApplicationBuilder();
			context.Log("UseExceptionHandler", _ => app.UseExceptionHandler(options));

			return context;
		}
	}
}
