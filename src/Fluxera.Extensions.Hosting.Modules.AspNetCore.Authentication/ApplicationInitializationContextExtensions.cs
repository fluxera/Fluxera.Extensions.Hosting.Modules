namespace Fluxera.Extensions.Hosting.Modules.AspNetCore.Authentication
{
	using JetBrains.Annotations;
	using Microsoft.AspNetCore.Authentication;
	using Microsoft.AspNetCore.Builder;

	/// <summary>
	///     Extension methods for the <see cref="IApplicationInitializationContext" /> type.
	/// </summary>
	[PublicAPI]
	public static class ApplicationInitializationContextExtensions
	{
		/// <summary>
		///     Adds the <see cref="AuthenticationMiddleware" /> to the specified <see cref="IApplicationBuilder" />, which enables
		///     authentication capabilities.
		/// </summary>
		/// <returns>A reference to this instance after the operation has completed.</returns>
		public static IApplicationInitializationContext UseAuthentication(this IApplicationInitializationContext context)
		{
			IApplicationBuilder app = context.GetApplicationBuilder();
			context.Log("UseAuthentication", _ => app.UseAuthentication());

			return context;
		}
	}
}
