namespace Fluxera.Extensions.Hosting.Modules.HttpClient
{
	using System;
	using System.Text.Json;
	using System.Text.Json.Serialization;
	using Fluxera.Enumeration.SystemTextJson;
	using Fluxera.StronglyTypedId.SystemTextJson;
	using Fluxera.ValueObject.SystemTextJson;
	using JetBrains.Annotations;

	/// <summary>
	///		Provides the JSON serializer options instance with custom type support.
	/// </summary>
	[PublicAPI]
	public static class HttpClientService
	{
		private static readonly Lazy<JsonSerializerOptions> JsonSerializerOptions = new Lazy<JsonSerializerOptions>(() =>
		{
			JsonSerializerOptions options = new JsonSerializerOptions
			{
				PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
				DictionaryKeyPolicy = JsonNamingPolicy.CamelCase,
				Converters =
				{
					new JsonStringEnumConverter()
				}
			};
			options.UseEnumeration();
			options.UsePrimitiveValueObject();
			options.UseStronglyTypedId();

			return options;
		});

		/// <summary>
		///		Gets the custom JSON serializer options.
		///		The custom options support smart enums, primitive value objects and strongly-typed IDs.
		/// </summary>
		public static JsonSerializerOptions Options => JsonSerializerOptions.Value;
	}
}
