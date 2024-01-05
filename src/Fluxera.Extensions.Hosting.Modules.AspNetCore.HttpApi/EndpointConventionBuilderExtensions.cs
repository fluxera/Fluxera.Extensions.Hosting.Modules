namespace Fluxera.Extensions.Hosting.Modules.AspNetCore.HttpApi
{
	using JetBrains.Annotations;
	using Microsoft.AspNetCore.Builder;
	using Microsoft.AspNetCore.Http;
	using Microsoft.AspNetCore.Routing;
	using SharpGrip.FluentValidation.AutoValidation.Endpoints.Filters;

	/// <summary>
	///		Extensions methods for the <see cref="IEndpointConventionBuilder"/> type.
	/// </summary>
	[PublicAPI]
	public static class EndpointConventionBuilderExtensions
	{
		/// <summary>
		///		Adds automatic validation support to an endpoint route.
		/// </summary>
		/// <param name="builder"></param>
		/// <returns></returns>
		public static RouteHandlerBuilder AddValidation(this RouteHandlerBuilder builder)
		{
			builder.AddEndpointFilter<DataAnnotationsValidationEndpointFilter>();

			builder.AddEndpointFilter<FluentValidationAutoValidationEndpointFilter>();

			return builder;
		}

		/// <summary>
		///		Adds automatic validation support to a endpoint group.
		/// </summary>
		/// <param name="builder"></param>
		/// <returns></returns>
		public static RouteGroupBuilder AddValidation(this RouteGroupBuilder builder)
		{
			builder.AddEndpointFilter<DataAnnotationsValidationEndpointFilter>();

			builder.AddEndpointFilter<FluentValidationAutoValidationEndpointFilter>();

			return builder;
		}
	}
}
