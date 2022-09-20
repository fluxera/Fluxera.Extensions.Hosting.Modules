namespace Fluxera.Extensions.Hosting.Modules.HttpClient
{
	using JetBrains.Annotations;
	using Microsoft.Extensions.DependencyInjection;

	/// <summary>
	///     A contract for contributors that register http client services.
	/// </summary>
	[PublicAPI]
	public interface IHttpClientServiceContributor
	{
		/// <summary>
		///     Add named http client services.
		/// </summary>
		/// <returns></returns>
		IHttpClientBuilder AddNamedHttpClientServices(IServiceConfigurationContext context);
	}
}
