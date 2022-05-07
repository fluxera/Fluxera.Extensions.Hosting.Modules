namespace Fluxera.Extensions.Hosting.Modules.AspNetCore.HttpApi
{
	using global::AspNetCore.ProblemDetails;
	using JetBrains.Annotations;

	/// <summary>
	///     A contract for contributors that configure the problem details middleware.
	/// </summary>
	[PublicAPI]
	public interface IProblemDetailsContributor
	{
		/// <summary>
		///     Configures the problem details options.
		/// </summary>
		/// <param name="options"></param>
		/// <param name="context"></param>
		void Configure(ProblemDetailsOptions options, IServiceConfigurationContext context);
	}
}
