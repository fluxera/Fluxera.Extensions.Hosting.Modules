namespace Fluxera.Extensions.Hosting.Modules.AspNetCore.HttpApi.OData
{
	using JetBrains.Annotations;
	using Microsoft.AspNetCore.Builder;
	using Microsoft.AspNetCore.OData;

	/// <summary>
	///     Extension methods for the <see cref="IApplicationInitializationContext" /> type.
	/// </summary>
	[PublicAPI]
	public static class ApplicationInitializationContextExtensions
	{
		/// <summary>
		///     Use OData route debug middleware. You can send request "~/$odata" after enabling this middleware.
		/// </summary>
		public static IApplicationInitializationContext UseODataRouteDebug(this IApplicationInitializationContext context)
		{
			WebApplication app = context.GetApplicationBuilder();
			context.Log("UseODataRouteDebug", _ => app.UseODataRouteDebug());

			return context;
		}

		/// <summary>
		///     Use OData batching middleware.
		/// </summary>
		public static IApplicationInitializationContext UseODataBatching(this IApplicationInitializationContext context)
		{
			WebApplication app = context.GetApplicationBuilder();
			context.Log("UseODataBatching", _ => app.UseODataBatching());

			return context;
		}
	}
}
