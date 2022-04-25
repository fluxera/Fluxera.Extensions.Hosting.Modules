namespace Fluxera.Extensions.Hosting.Modules.AspNetCore.HttpApi.OData.Contributors
{
	using Fluxera.Extensions.Hosting.Modules.AspNetCore.Authorization;
	using JetBrains.Annotations;
	using Microsoft.AspNetCore.Mvc.ApplicationModels;
	using Microsoft.AspNetCore.OData.Routing.Controllers;

	[UsedImplicitly]
	internal sealed class AuthorizeContributor : IAuthorizeContributor
	{
		/// <inheritdoc />
		public bool AllowAnonymous(ControllerModel controllerModel)
		{
			return controllerModel.ControllerType == typeof(MetadataController);
		}
	}
}
