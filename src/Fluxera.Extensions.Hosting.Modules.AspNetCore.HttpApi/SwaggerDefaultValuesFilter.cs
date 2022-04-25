namespace Fluxera.Extensions.Hosting.Modules.AspNetCore.HttpApi
{
	using System;
	using System.Linq;
	using System.Text.Json;
	using JetBrains.Annotations;
	using Microsoft.AspNetCore.Mvc.ApiExplorer;
	using Microsoft.AspNetCore.Mvc.ModelBinding;
	using Microsoft.OpenApi.Models;
	using Swashbuckle.AspNetCore.SwaggerGen;

	/// <summary>
	///     Represents the OpenAPI/Swashbuckle operation filter used to document information provided, but not used.
	/// </summary>
	/// <remarks>
	///     This <see cref="IOperationFilter" /> is only required due to bugs in the <see cref="SwaggerGenerator" />.
	///     Once they are fixed and published, this class can be removed.
	/// </remarks>
	[UsedImplicitly]
	internal sealed class SwaggerDefaultValuesFilter : IOperationFilter
	{
		/// <inheritdoc />
		public void Apply(OpenApiOperation operation, OperationFilterContext context)
		{
			ApiDescription apiDescription = context.ApiDescription;

			operation.Deprecated |= apiDescription.IsDeprecated();

			// REF: https://github.com/domaindrivendev/Swashbuckle.AspNetCore/issues/1752#issue-663991077
			foreach(ApiResponseType responseType in context.ApiDescription.SupportedResponseTypes)
			{
				// REF: https://github.com/domaindrivendev/Swashbuckle.AspNetCore/blob/b7cf75e7905050305b115dd96640ddd6e74c7ac9/src/Swashbuckle.AspNetCore.SwaggerGen/SwaggerGenerator/SwaggerGenerator.cs#L383-L387
				string responseKey = responseType.IsDefaultResponse ? "default" : responseType.StatusCode.ToString();
				OpenApiResponse response = operation.Responses[responseKey];

				foreach(string contentType in response.Content.Keys)
				{
					if(!responseType.ApiResponseFormats.Any(x => x.MediaType == contentType))
					{
						response.Content.Remove(contentType);
					}
				}
			}

			if(operation.Parameters == null)
			{
				return;
			}

			// REF: https://github.com/domaindrivendev/Swashbuckle.AspNetCore/issues/412
			// REF: https://github.com/domaindrivendev/Swashbuckle.AspNetCore/pull/413
			foreach(OpenApiParameter parameter in operation.Parameters)
			{
				ApiParameterDescription description = apiDescription.ParameterDescriptions.First(p => p.Name == parameter.Name);

				if(parameter.Description == null)
				{
					parameter.Description = description.ModelMetadata?.Description;
				}

				if((parameter.Schema.Default == null) &&
				   (description.DefaultValue != null) &&
				   description.DefaultValue is not DBNull &&
				   description.ModelMetadata is ModelMetadata modelMetadata)
				{
					// REF: https://github.com/Microsoft/aspnet-api-versioning/issues/429#issuecomment-605402330
					string json = JsonSerializer.Serialize(description.DefaultValue, modelMetadata.ModelType);
					parameter.Schema.Default = OpenApiAnyFactory.CreateFromJson(json);
				}

				parameter.Required |= description.IsRequired;
			}
		}
	}
}
