namespace Fluxera.Extensions.Hosting.Modules.AspNetCore.Authorization
{
	using JetBrains.Annotations;
	using Microsoft.AspNetCore.Mvc.ApplicationModels;

	/// <summary>
	///     A contract for a authorize contributor.
	/// </summary>
	[PublicAPI]
	public interface IAuthorizeContributor
	{
		/// <summary>
		///     Allow anonymous access for the given controller.
		/// </summary>
		/// <param name="controllerModel"></param>
		/// <param name="context"></param>
		/// <returns></returns>
		bool AllowAnonymous(ControllerModel controllerModel, IServiceConfigurationContext context);
	}
}
