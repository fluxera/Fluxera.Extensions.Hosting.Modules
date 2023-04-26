namespace Fluxera.Extensions.Hosting.Modules.HttpClient
{
	using Fluxera.Extensions.Http;
	using JetBrains.Annotations;
	using Microsoft.Extensions.DependencyInjection;

	/// <summary>
	///     A contract for contributors that may modify http client my adding to the <see cref="IHttpClientBuilder"/>.
	/// </summary>
	[PublicAPI]
	public interface IHttpClientBuilderContributor
	{
		/// <summary>
		///     Configure the named http client builder.
		/// </summary>
		/// <returns></returns>
		void ConfigureHttpClientBuilder(IHttpClientBuilder builder, RemoteService remoteService, IServiceConfigurationContext context);
	}
}
