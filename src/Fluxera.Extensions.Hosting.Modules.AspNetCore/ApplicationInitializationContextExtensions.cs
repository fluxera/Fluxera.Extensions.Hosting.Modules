namespace Fluxera.Extensions.Hosting.Modules.AspNetCore
{
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
		public static IApplicationInitializationContext MapControllers(this IApplicationInitializationContext context)
		{
			WebApplication app = context.GetApplicationBuilder();
			context.Log("MapControllers", _ => app.MapControllers());

			return context;
		}
	}
}
