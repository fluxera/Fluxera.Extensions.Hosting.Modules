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
		///     Add a named http client service.
		/// </summary>
		/// <returns></returns>
		IHttpClientBuilder AddNamedHttpClientService(IServiceConfigurationContext context);
	}
}
