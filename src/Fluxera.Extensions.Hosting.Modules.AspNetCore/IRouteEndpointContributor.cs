namespace Fluxera.Extensions.Hosting.Modules.AspNetCore
{
	using JetBrains.Annotations;
	using Microsoft.AspNetCore.Routing;

	/// <summary>
	///     A contract for contributors that configure the endpoint routes.
	/// </summary>
	[PublicAPI]
	public interface IRouteEndpointContributor
	{
		/// <summary>
		///     The position at which this contributor is executed.
		/// </summary>
		int Position => 0;

		/// <summary>
		///     Maps the route in the route builder.
		/// </summary>
		/// <param name="routeBuilder"></param>
		void MapRoute(IEndpointRouteBuilder routeBuilder);
	}
}
