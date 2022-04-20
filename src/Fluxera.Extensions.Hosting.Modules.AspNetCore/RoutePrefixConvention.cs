namespace Fluxera.Extensions.Hosting.Modules.AspNetCore
{
	using System.Linq;
	using Microsoft.AspNetCore.Mvc.ApplicationModels;
	using Microsoft.AspNetCore.Mvc.Routing;

	internal sealed class RoutePrefixConvention : IApplicationModelConvention
	{
		private readonly AttributeRouteModel routePrefix;

		public RoutePrefixConvention(IRouteTemplateProvider route)
		{
			this.routePrefix = new AttributeRouteModel(route);
		}

		public void Apply(ApplicationModel application)
		{
			foreach(SelectorModel selector in application.Controllers.SelectMany(c => c.Selectors))
			{
				selector.AttributeRouteModel = selector.AttributeRouteModel != null
					? AttributeRouteModel.CombineAttributeRouteModel(this.routePrefix, selector.AttributeRouteModel)
					: this.routePrefix;
			}
		}
	}
}
