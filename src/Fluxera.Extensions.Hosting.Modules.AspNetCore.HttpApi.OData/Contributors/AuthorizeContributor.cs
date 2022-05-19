namespace Fluxera.Extensions.Hosting.Modules.AspNetCore.HttpApi.OData.Contributors
{
	using Asp.Versioning.Controllers;
	using Fluxera.Extensions.Hosting.Modules.AspNetCore.Authorization;
	using JetBrains.Annotations;
	using Microsoft.AspNetCore.Mvc.ApplicationModels;
	using Microsoft.AspNetCore.OData.Routing.Controllers;

	[UsedImplicitly]
	internal sealed class AuthorizeContributor : IAuthorizeContributor
	{
		/// <inheritdoc />
		public bool AllowAnonymous(ControllerModel controllerModel, IServiceConfigurationContext context)
		{
			return
				controllerModel.ControllerType == typeof(MetadataController) ||
				controllerModel.ControllerType == typeof(VersionedMetadataController);
		}
	}
}
