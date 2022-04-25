//namespace Fluxera.Extensions.Hosting.Modules.AspNetCore.HttpApi.OData
//{
//	using JetBrains.Annotations;
//	using Microsoft.AspNetCore.Mvc.ApiExplorer;
//	using Microsoft.AspNetCore.Mvc.Controllers;
//	using Microsoft.OpenApi.Models;
//	using Swashbuckle.AspNetCore.SwaggerGen;

//	[UsedImplicitly]
//	internal sealed class ODataMetadataControllerDocumentFilter : IDocumentFilter
//	{
//		public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
//		{
//			// remove controller
//			foreach(ApiDescription apiDescription in context.ApiDescriptions)
//			{
//				ControllerActionDescriptor actionDescriptor = (ControllerActionDescriptor)apiDescription.ActionDescriptor;
//				if(actionDescriptor.ControllerName is "Metadata" or "VersionedMetadata")
//				{
//					swaggerDoc.Paths.Remove($"/{apiDescription.RelativePath}");
//				}
//			}

//			// remove schemas
//			foreach((string key, _) in swaggerDoc.Components.Schemas)
//			{
//				if(key.Contains("Edm") || key.Contains("OData"))
//				{
//					swaggerDoc.Components.Schemas.Remove(key);
//				}
//			}
//		}
//	}
//}


