namespace Fluxera.Extensions.Hosting.Modules.AspNetCore.HttpApi
{
	using System;
	using System.Linq;
	using Asp.Versioning.ApiExplorer;
	using JetBrains.Annotations;
	using MadEyeMatt.AspNetCore.ProblemDetails;
	using Microsoft.AspNetCore.Builder;
	using Microsoft.Extensions.DependencyInjection;

	/// <summary>
	///     Extension methods for the <see cref="IApplicationInitializationContext" /> type.
	/// </summary>
	[PublicAPI]
	public static class ApplicationInitializationContextExtensions
	{
		/// <summary>
		///     Register the SwaggerUI middleware with optional setup action for DI-injected options.
		/// </summary>
		public static IApplicationInitializationContext UseSwaggerUI(this IApplicationInitializationContext context)
		{
			IApiVersionDescriptionProvider versionDescriptionProvider = context.ServiceProvider.GetService<IApiVersionDescriptionProvider>();

			IApplicationBuilder app = context.GetApplicationBuilder();
			context.Log("UseSwaggerUI", _ => app.UseSwaggerUI(options =>
			{
				if(versionDescriptionProvider != null && versionDescriptionProvider.ApiVersionDescriptions.Any())
				{
					// Add swagger endpoint for every versioned API.
					foreach(ApiVersionDescription description in versionDescriptionProvider.ApiVersionDescriptions)
					{
						string url = $"/docs/{description.GroupName}/openapi.json";
						string name = description.GroupName.ToUpperInvariant();
						options.SwaggerEndpoint(url, name);
					}
				}
				else
				{
					// Add a single swagger endpoint.
					options.SwaggerEndpoint("/docs/v1/openapi.json", "V1");
				}
			}));

			return context;
		}

		/// <summary>
		///     Adds the <see cref="ProblemDetailsMiddleware" /> to the application pipeline.
		/// </summary>
		/// <exception cref="InvalidOperationException">
		///     Thrown if <see cref="MvcBuilderExtensions.AddProblemDetails(IMvcBuilder, Action{ProblemDetailsOptions})" />
		///     hasn't been called.
		/// </exception>
		public static IApplicationInitializationContext UseProblemDetails(this IApplicationInitializationContext context)
		{
			IApplicationBuilder app = context.GetApplicationBuilder();
			context.Log("UseProblemDetails", _ => app.UseProblemDetails());
			return context;
		}
	}
}
