namespace Fluxera.Extensions.Hosting.Modules.AspNetCore.Swagger
{
	using System;
	using System.Reflection;
	using JetBrains.Annotations;
	using Microsoft.AspNetCore.Mvc.ApiExplorer;
	using Microsoft.OpenApi.Models;
	using Swashbuckle.AspNetCore.SwaggerGen;

	[UsedImplicitly]
	internal sealed class DeprecatedOperationFilter : IOperationFilter
	{
		public void Apply(OpenApiOperation operation, OperationFilterContext context)
		{
			bool isObsolete = context.MethodInfo.GetCustomAttribute<ObsoleteAttribute>() != null;
			operation.Deprecated = context.ApiDescription.IsDeprecated() || isObsolete;
		}
	}
}
