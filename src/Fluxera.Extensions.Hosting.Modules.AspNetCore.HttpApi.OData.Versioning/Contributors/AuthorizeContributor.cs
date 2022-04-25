namespace Fluxera.Extensions.Hosting.Modules.AspNetCore.HttpApi.OData.Versioning.Contributors
{
	using Asp.Versioning.Controllers;
	using Fluxera.Extensions.Hosting.Modules.AspNetCore.Authorization;
	using JetBrains.Annotations;
	using Microsoft.AspNetCore.Mvc.ApplicationModels;

	[UsedImplicitly]
	internal sealed class AuthorizeContributor : IAuthorizeContributor
	{
		/// <inheritdoc />
		public bool AllowAnonymous(ControllerModel controllerModel)
		{
			return controllerModel.ControllerType == typeof(VersionedMetadataController);
		}
	}
}
