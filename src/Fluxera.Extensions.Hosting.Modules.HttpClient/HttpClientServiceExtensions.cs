namespace Fluxera.Extensions.Hosting.Modules.HttpClient
{
	using System.Text.Json;
	using Fluxera.Extensions.Http;
	using JetBrains.Annotations;

	/// <summary>
	///		Extension methods for the <see cref="IHttpClientService"/> type.
	/// </summary>
	[PublicAPI]
	public static class HttpClientServiceExtensions
	{
		/// <summary>
		///		Gets the custom JSON serializer options which will be used in HTTP client services.
		///		The custom options support smart enums, primitive value objects and strongly-typed IDs.
		/// </summary>
		/// <param name="service"></param>
		/// <returns></returns>
		public static JsonSerializerOptions GetJsonSerializerOptions(this IHttpClientService service)
		{
			return HttpClientService.Options;
		}
	}
}
