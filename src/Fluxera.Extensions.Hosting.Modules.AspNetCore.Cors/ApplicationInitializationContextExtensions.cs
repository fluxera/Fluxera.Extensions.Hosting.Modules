namespace Fluxera.Extensions.Hosting.Modules.AspNetCore.Cors
{
	using JetBrains.Annotations;
	using Microsoft.AspNetCore.Builder;

	/// <summary>
	///     Extension methods for the <see cref="IApplicationInitializationContext" /> type.
	/// </summary>
	[PublicAPI]
	public static class ApplicationInitializationContextExtensions
	{
		/// <summary>
		///     Adds a CORS middleware to your web application pipeline to allow cross domain requests.
		/// </summary>
		/// <param name="context"></param>
		/// <returns></returns>
		public static IApplicationInitializationContext UseCors(this IApplicationInitializationContext context)
		{
			IApplicationBuilder app = context.GetApplicationBuilder();
			context.Log("UseCors", _ => app.UseCors());

			return context;
		}
	}
}
