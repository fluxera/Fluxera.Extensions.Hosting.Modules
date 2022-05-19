namespace Fluxera.Extensions.Hosting.Modules.AspNetCore.HttpApi
{
	using System.Linq;
	using JetBrains.Annotations;
	using Microsoft.AspNetCore.Mvc.ApiExplorer;
	using Microsoft.OpenApi.Models;
	using Swashbuckle.AspNetCore.SwaggerGen;

	[UsedImplicitly]
	internal sealed class DefaultValuesFilter : IOperationFilter
	{
		public void Apply(OpenApiOperation operation, OperationFilterContext context)
		{
			ApiDescription apiDescription = context.ApiDescription;

			operation.Deprecated = apiDescription.IsDeprecated();

			if(operation.Parameters == null)
			{
				return;
			}

			foreach(OpenApiParameter parameter in operation.Parameters)
			{
				ApiParameterDescription description = apiDescription.ParameterDescriptions.First(p => p.Name == parameter.Name);

				parameter.Description ??= description.ModelMetadata.Description;
				parameter.Required |= description.IsRequired;
			}
		}
	}
}
