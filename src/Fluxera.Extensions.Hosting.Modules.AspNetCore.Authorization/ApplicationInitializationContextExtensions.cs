namespace Fluxera.Extensions.Hosting.Modules.AspNetCore.Authorization
{
	using JetBrains.Annotations;
	using Microsoft.AspNetCore.Authorization;
	using Microsoft.AspNetCore.Builder;

	/// <summary>
	///     Extension methods for the <see cref="IApplicationInitializationContext" /> type.
	/// </summary>
	[PublicAPI]
	public static class ApplicationInitializationContextExtensions
	{
		/// <summary>
		///     Adds the <see cref="AuthorizationMiddleware" /> to the specified <see cref="IApplicationBuilder" />, which enables
		///     authorization capabilities.
		///     <para>
		///         When authorizing a resource that is routed using endpoint routing, this call must appear between the calls to
		///         <c>app.UseRouting()</c> and <c>app.UseEndpoints(...)</c> for the middleware to function correctly.
		///     </para>
		/// </summary>
		/// <returns>A reference to <paramref name="context" /> after the operation has completed.</returns>
		public static IApplicationInitializationContext UseAuthorization(this IApplicationInitializationContext context)
		{
			IApplicationBuilder app = context.GetApplicationBuilder();
			context.Log("UseAuthorization", _ => app.UseAuthorization());

			return context;
		}
	}
}
