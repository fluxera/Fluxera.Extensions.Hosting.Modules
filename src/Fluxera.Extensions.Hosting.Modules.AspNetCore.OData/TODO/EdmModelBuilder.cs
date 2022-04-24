//namespace Fluxera.Extensions.Hosting.Modules.AspNetCore.OData
//{
//	using System;
//	using System.Collections.Generic;
//	using JetBrains.Annotations;
//	using Microsoft.Extensions.DependencyInjection;
//	using Microsoft.OData.Edm;
//	using Microsoft.OData.ModelBuilder;

//	[UsedImplicitly]
//	internal sealed class EdmModelBuilder : IEdmModelBuilder
//	{
//		private readonly IServiceProvider serviceProvider;

//		public EdmModelBuilder(IServiceProvider serviceProvider)
//		{
//			this.serviceProvider = serviceProvider;
//		}

//		public IEdmModel[] GetEdmModels()
//		{
//			ODataConventionModelBuilder builder = new CustomODataModelBuilder();

//			// Try to get the registered context instance from the container or create a default one.
//			EdmModelContext context = this.serviceProvider.GetService<EdmModelContext>() ?? new EdmModelContext();

//			IEnumerable<IEdmModelContributor> contributors = this.serviceProvider.GetServices<IEdmModelContributor>();
//			foreach(IEdmModelContributor contributor in contributors)
//			{
//				contributor.Configure(builder, context);
//			}

//			return new IEdmModel[] { builder.GetEdmModel() };
//		}
//	}
//}


